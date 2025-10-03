
using BrightMinds.Api.Errors;
using BrightMinds.Api.Response;
using BrightMinds.Core.Models;
using BrightMinds.Services.Dtos.IdentityDtos;
using BrightMinds.Services.Helpers;
using BrightMinds.Services.Helpers.Emails;
using BrightMinds.Services.IServices;
using BrightMinds.Services.SpecificationParams;
using BrightMinds.Services.Dtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrightMinds.Api.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInmanager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager
            , ITokenService tokenService,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInmanager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }


        [HttpGet]
        public async Task<ActionResult> GetUsersAsync([FromQuery]UserSpecParams userParams )
        {
            var count = await _userManager.Users
                 .Where(u => string.IsNullOrEmpty(userParams.SearchName) ||
                (u.FirstName + u.LastName).ToLower().Contains(userParams.SearchName.ToLower()))
                .CountAsync();
            var users = await _userManager.Users.AsQueryable()
                .Where(u => string.IsNullOrEmpty(userParams.SearchName) ||
                (u.FirstName + u.LastName).ToLower().Contains(userParams.SearchName.ToLower()))
                .OrderBy(o=>o.Id)
            .Skip((userParams.PageIndex - 1) * userParams.PageSize)
                .Take(userParams.PageSize).ToListAsync();

            //        var users = await _userManager.Users
            //.Where(u => string.IsNullOrEmpty(userParams.SearchName) ||
            //    EF.Functions.Like((u.FirstName + " " + u.LastName).ToLower(), "%" + userParams.SearchName.ToLower() + "%"))
            //.Skip((userParams.PageIndex - 1) * userParams.PageSize)
            //.Take(userParams.PageSize)
            //.ToListAsync();




            var usersToReturn = users.Adapt<List<UserDto>>();
            //var usersToReturn = users.AsQueryable().ProjectToType<UserDto>().ToList();  

            return Ok(new
            {
                Messaget = "success",
                Data = new Pagination(userParams.PageSize,userParams.PageIndex,count, usersToReturn)
            });
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user==null)
                return NotFound(new ApiResponse(404,"User not found"));
            var userToReturn = user.Adapt<UserDto>();
            //var usersToReturn = users.AsQueryable().ProjectToType<UserDto>().ToList();  

            return Ok(new
            {
                Messaget = "success",
                Data = userToReturn
            });
        }






        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return Unauthorized(new ApiResponse(401, "Invalid username or password"));


            var result = await _signInmanager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401, "Invalid username or password"));
            if (!user.EmailConfirmed)
                return Unauthorized(new ApiResponse(401, "email not confirmed"));
            return Ok(new
            {
                message = "success",
                Token = await _tokenService.GenerateToken(user, _userManager),
                user = new
                {
                    Email = model.Email,
                    DisplayName = $"{user.FirstName} {user.LastName}",
                }
            });
        }



        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromForm] UserRegisterDto model)
        {

            if (CheckUserExist(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse()
                {
                    Errors = new string[]
                {
                    "Email is already exist"
                }
                });
            var user = new AppUser()
            {
                UserName = model.Email.Split("@")[0],
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,

                PhoneNumber = model.Mobile
            };
            string imageUrl = "";
            if (model.Image != null) {
                imageUrl = await DocumentSetting.UploadFile(model.Image, "UsersImages");

                user.ImageCover = imageUrl;
            }
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));
            return Ok(new
            {
                message = "success",
                //Token = await _tokenService.GenerateToken(user, _userManager),
                user = new UserDto()
                {
                    Email = model.Email,
                    DisplayName = $"{model.FirstName} {model.LastName}",
                }
            });
        }
        [HttpPost("authnticate-email")]
        public async Task<ActionResult> AuthnticateEmail([FromBody] CheckEmailDto checkEmail)
        {
            var user = await _userManager.FindByEmailAsync(checkEmail.Email);
            if (user == null)
                return NotFound(new ApiResponse(404, "User not found"));
            if (await _userManager.IsEmailConfirmedAsync(user))
                return BadRequest(new ApiResponse(400, "email is confirmed before"));
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string?>()
          {
              {"token",token },
              {"email",checkEmail.Email}
          };
            var callback = QueryHelpers.AddQueryString(checkEmail.ClientUrl, param);
            var displayName = $"{user.FirstName} {user.LastName}";
            var confirmEmail = new ConfirmEmail(checkEmail.Email, displayName, callback);

            await _emailService.SendEmail(confirmEmail);
            return Ok(new ApiResponse(201));
        }


        [HttpPost("confirm-email")]
        //[HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmEmail)
        {
            var user = await _userManager.FindByEmailAsync(confirmEmail.Email);
            if (user == null)
                return NotFound(new ApiResponse(404,"user not found"));
                

            var result = await _userManager.ConfirmEmailAsync(user, confirmEmail.Token);
            if (result.Succeeded)
                return Ok(new ApiResponse(200, "Email confirmed successfully"));



            return BadRequest(new ApiResponse(400, "Invalid email confirmation token"));
            
        }




        [HttpPost("forget-password")]
        public async Task<ActionResult> ForgetPassword([FromBody] CheckEmailDto checkEmail)
        {
            var user = await _userManager.FindByEmailAsync(checkEmail.Email);
            if (user == null)
                return NotFound(new ApiResponse(404));
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string?>()
          {
              {"token",token },
              {"email",checkEmail.Email}
          };
            var callback = QueryHelpers.AddQueryString(checkEmail.ClientUrl, param);
            var displayName = $"{user.FirstName} {user.LastName}";
            var confirmEmail = new ForgetPasswordEmail(checkEmail.Email, displayName, callback);

            await _emailService.SendEmail(confirmEmail);
            return Ok(new ApiResponse(201));
        }


        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                return NotFound(new ApiResponse(404));
                

            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token,resetPassword.Password);
            if (result.Succeeded)
                return Ok(new ApiResponse(201, "Password reseted succesfully"));


            return BadRequest(new ApiResponse(400, "an error occured while resetting password"));
         
            
        }





        [HttpGet("check")]
        public async Task<ActionResult<bool>>CheckUserExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUser(string userId, [FromBody] UserUpdateDto user)
        {
            if (userId==null||userId != user.Id)
                return BadRequest(new ApiResponse(400));
            var appUser =await _userManager.FindByIdAsync(userId);   
            if (appUser==null)
                return BadRequest(new ApiResponse(404,"User Not Found"));
         
            appUser.PhoneNumber=user.Mobile;
            if (user.Image != null)
            {
               await DocumentSetting.DeleteFile(appUser.ImageCover, "UserImages");
               appUser.ImageCover=await DocumentSetting.UploadFile(user.Image, "UserImages");
            }
            await _userManager.UpdateAsync(appUser);

            return Ok(new
            {
                message = "success",
                user = new UserDto()
                {
                
                    Email = appUser.Email,
                    DisplayName = $"{appUser.FirstName} {appUser.LastName}",
                  
                 
                    Mobile = appUser.PhoneNumber,
                    ImageCover = appUser.ImageCover,
                     
                }
            });
        }
        [Authorize]
        [HttpPut("changepassword")]
        public async Task<ActionResult>ChangePassword([FromBody]ChangePasswordDto changePassword)
        {
            var name = User.Identity.Name;
            var user=await _userManager.FindByNameAsync(name);
            var checkFlag = await _userManager.CheckPasswordAsync(user, changePassword.OldPassword);
            if (!checkFlag)
                return BadRequest(new ApiResponse(400, "Invalid old Password"));
            var result=await _userManager.ChangePasswordAsync(user,changePassword.OldPassword,changePassword.NewPassword);
            if(!result.Succeeded)
                return BadRequest(new ApiResponse(400, "an error occured  while Updating Password"));
           else return Ok(new ApiResponse(200));
        }


        [HttpDelete("{userId}")]
        public async Task<ActionResult>DeleteUser(string userId)
        {
            var user=await _userManager.FindByIdAsync(userId);
            if (user==null)
                return NotFound(new ApiResponse(404));  

            var result= await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));
            return Ok(new ApiResponse(200,"Deleted successfully"));
        }

        


    }
}

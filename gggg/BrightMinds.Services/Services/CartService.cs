using AutoMapper;
using Azure;

using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;
using BrightMinds.Core.Specifications;
using BrightMinds.Infrastructure.Repositories;
using BrightMinds.Services.Dtos.CartCdos;
using BrightMinds.Services.IServices;
using BrightMinds.Services.ServiceResponse.Cart;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Services
{
    public class CartService :ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService( IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<CartResponse> AddToCartAsync(CartItemDto cartItem, string userId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            var course = await _unitOfWork.CourseRepository.GetAsync(cartItem.CourseId);
            var response = new CartResponse();
            if (course == null)
            {
                response.Success = false;
                response.Message = "Book not found";
                response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (cart == null)
            {
                var newCart = new Cart()
                {
                    UserId =userId
                 };
                await _unitOfWork.CartRepository.Add(newCart);
                await _unitOfWork.CompleteAsync();
                var cartItemMapped = cartItem.Adapt<CartItem>();
                cartItemMapped.Price = course.Price;
                cartItemMapped.CartId = newCart.Id;
                await _unitOfWork.CartItemRepository.Add(cartItemMapped);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                var cartItemMapped = cartItem.Adapt<CartItem>();
                //cartItemMapped.Price =  course.Price;
                cartItemMapped.CartId = cart.Id;
                await _unitOfWork.CartItemRepository.Add(cartItemMapped);
                await _unitOfWork.CompleteAsync();
            }
            response.Message = "Created Successfully";
            response.Success = true;
            response.StatusCode = (int)HttpStatusCode.Created;
            return response;
        }

        public async Task<CartResponse> GetCartItemsAsync(string userId)
        {
            var response=new CartResponse();
            var cart=await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            var spec = new CartItemSpecifications();
            var cartItems=await _unitOfWork.CartItemRepository.GetCartItemsByCartId(spec);
            if(cartItems == null) {
                response.Success = false;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Message = "No CartItems Found";
            }
            else
            {
                response.Success = true;
                response.StatusCode = (int)HttpStatusCode.OK;
                var cartItemsReturn=cartItems.Adapt<IReadOnlyList<CartItemWithCourseDto>>();
                var data = new CartWithCartItemsDto()
                {
                    Id = cart.Id,
                    Items = cartItemsReturn
                };
                response.Data=data;
                response.Data.Id= cart.Id;  
            }
            return response;
        }


        public async Task<CartResponse>ClearCartAsync(int cartId)
        {
            var response=new CartResponse();
            try
            {
                await _unitOfWork.CartItemRepository.ClearCartItems(cartId);
                await _unitOfWork.CompleteAsync();
                response.Success = true;
                response.Message = "Cart Cleared successfully";
                response.StatusCode = (int)HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Message = "un error occured while Clearing Cart";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Success = false;
            }
        
            return response;
        }

        public async Task<CartResponse> DeleteCartItemAsync(int cartItemId)
        {
            var response = new CartResponse();
            var cartItem = await _unitOfWork.CartItemRepository.GetAsync(cartItemId);
            if (cartItem is null)
            {
                response.Success = false;
                response.Message = "CartItem isn't exist";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;    
            }
            try
            {
                _unitOfWork.CartItemRepository.Delete(cartItem);
                await _unitOfWork.CompleteAsync();
                response.Success = true;
                response.Message = "Deleted Successfully";
                response.StatusCode = (int)HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Message = "un error occured while deleting CartItem";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Success = false;
            }

            return response;
        }
        public async Task<CartResponse>UpdateCartItemQuantity(int id,int quantity)
        {
            var cartItem=await _unitOfWork.CartItemRepository.GetAsync(id);
            var response=new CartResponse();
            if(cartItem is null) 
            {
                response.Success = false;
                response.Message = "CartItem isn't exist";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if(quantity<=0)
            {
                response.Success = false;
                response.Message = "CartItem Quantity must be greater than 0";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                //cartItem.Quantity =quantity;
                _unitOfWork.CartItemRepository.Update(cartItem);
                await _unitOfWork.CompleteAsync();
                response.Success = true;
                response.StatusCode = (int)HttpStatusCode.NoContent;
                response.Message = "Updated successfully";
            }
            return response;
        }


    }
}

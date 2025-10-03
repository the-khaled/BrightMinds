using BrightMinds.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using BrightMinds.Infrastructure.Repositories;
using Library.Core.Interfaces;

namespace BrightMinds.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BrightMindsContext _context;

        // Lazy-initialized repositories
        private readonly Lazy<IFeedbackRepository> _feedbackRepository;
        private readonly Lazy<IUserRepository> _IUserRepository; 
        private readonly Lazy<ICouponRepository> _couponRepository;
        private readonly Lazy<IInstructorRepository> _instructorRepository;
        private readonly Lazy<ICourseRepository> _courseRepository;
        private readonly Lazy<ISectionRepository> _sectionRepository;
        private readonly Lazy<IVideoRepository> _videoRepository;
        private readonly Lazy<ICartRepository> _cartRepository;
        private readonly Lazy<ICartItemRepository> _cartItemRepository;
        private readonly Lazy<ICateogryRepository> _cateogryRepository;
        private readonly Lazy<ICouponUsageRepository> _couponUsageRepository;
        public UnitOfWork(BrightMindsContext context)
        {
            _context = context;

            // Initialize each repository lazily with context
            _feedbackRepository=new Lazy<IFeedbackRepository> (()=>new FeedbackRepository(_context));
            _IUserRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _couponRepository = new Lazy<ICouponRepository>(() => new CouponRepository(_context));
            _instructorRepository = new Lazy<IInstructorRepository>(() => new InstructorRepository(_context));
            _courseRepository= new Lazy<ICourseRepository>(() => new CourseRepository(_context));
            _sectionRepository= new Lazy<ISectionRepository>(() => new SectionRepository(_context));
            _videoRepository= new Lazy<IVideoRepository>(() => new VideoRepository(_context));
            _cartItemRepository = new Lazy<ICartItemRepository>(()=>new CartItemRepositrory(_context));
            _cartRepository = new Lazy<ICartRepository>(()=>new CartRepository(_context));
            _cateogryRepository= new Lazy<ICateogryRepository>(()=>new CategoryRepository(_context));
            _couponUsageRepository = new Lazy<ICouponUsageRepository>(() => new CouponUsageRepository(_context));
        }

        // Properties that access the lazy-loaded repositories
        public IFeedbackRepository FeedbackRepository => _feedbackRepository.Value;
        public IUserRepository UserRepository=> _IUserRepository.Value;
        public ICouponRepository CouponRepository => _couponRepository.Value;
        public IInstructorRepository InstructorRepository => _instructorRepository.Value;

        public ICourseRepository CourseRepository => _courseRepository.Value ;

        public ISectionRepository SectionRepository =>_sectionRepository.Value;

        public IVideoRepository VideoRepository => _videoRepository.Value;
        public ICartRepository CartRepository => _cartRepository.Value;
        public ICartItemRepository CartItemRepository => _cartItemRepository.Value;
        public ICateogryRepository CateogryRepository=> _cateogryRepository.Value;

        public ICouponUsageRepository CouponUsageRepository => _couponUsageRepository.Value;
        // Transaction and Save operations
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task RollBackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }

}

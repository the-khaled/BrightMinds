using Library.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IFeedbackRepository FeedbackRepository { get; }
        IUserRepository UserRepository { get; }
        ICouponRepository CouponRepository { get; }
        ICouponUsageRepository CouponUsageRepository { get; }
        IInstructorRepository InstructorRepository { get; }
        ICourseRepository CourseRepository { get; }
        ISectionRepository SectionRepository { get; }
        IVideoRepository VideoRepository { get; }
        ICartItemRepository CartItemRepository { get; }
        ICartRepository CartRepository { get; }
        ICateogryRepository CateogryRepository { get; }

        Task RollBackAsync();
        Task CommitAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> CompleteAsync();
    }
}

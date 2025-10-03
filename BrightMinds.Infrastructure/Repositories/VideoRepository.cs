using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;
using BrightMinds.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Infrastructure.Repositories
{
    public class VideoRepository:GenericRepository<Video>,IVideoRepository
    {
        public VideoRepository(BrightMindsContext context ):base(context)
        {
            
        }
    }
}

using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShapingAPI.Entities;
using ShapingAPI.Infrastructure.Core;
using ShapingAPI.Infrastructure.Data.Repositories;
using ShapingAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ShapingAPI.Controllers
{
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        #region Properties
        private readonly IAlbumRepository _albumRepository;
        private Expression<Func<Album, object>>[] includeProperties;
        private const int maxSize = 50;
        private IMapper _mapper;
        #endregion

        #region Constructor
        public AlbumsController(IAlbumRepository albumRepository,IMapper mapper)
        {
            _albumRepository = albumRepository;

            includeProperties = Expressions.LoadAlbumNavigations();
            _mapper = mapper;
        }
        #endregion

        #region Actions
        public ActionResult Get(string props = null, int page = 1, int pageSize = maxSize)
        {
            try
            {
                var _albums = _albumRepository.LoadAll().Skip((page - 1) * pageSize).Take(pageSize);

                var test = _albums.ToList();

                var _albumsVM = _mapper.Map<IEnumerable<AlbumViewModel>>(_albums);

                JToken _jtoken = TokenService.CreateJToken(_albumsVM, props);

                return Ok(_jtoken);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Route("{albumId}")]
        public ActionResult Get(int albumId, string props = null)
        {
            try
            {
                var _album = _albumRepository.Get(t => t.AlbumId == albumId, includeProperties);

                if (_album == null)
                {
                    return BadRequest();
                }

                var _albumVM = Mapper.Map<Album, AlbumViewModel>(_album);

                JToken _jtoken = TokenService.CreateJToken(_albumVM, props);

                return Ok(_jtoken);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion
    }
}

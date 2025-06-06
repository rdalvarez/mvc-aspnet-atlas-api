﻿using Microsoft.AspNetCore.Mvc;
using PrimeraApiRest.Data;
using PrimeraApiRest.Models;
using PrimeraApiRest.Models.Dto;

namespace PrimeraApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtlasPhotoController : Controller
    {
        private readonly AppDbContext _context;
        private ResponseDto _response;

        public AtlasPhotoController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDto();
        }

        [HttpGet("GetPhoto")]
        public ResponseDto GetPhotos()
        {
            try
            {
                IEnumerable<AtlasPhoto> photos = _context.Photos.ToList<AtlasPhoto>();
                _response.Data = photos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet("GetPhotoById/{id}")]
        public ResponseDto GetPhotoById(int id)
        {
            try
            {
                var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
                _response.Data = photo;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet("GetPhotoByTitle/{title}")]
        public ResponseDto GetPhotoByTitle(string title)
        {
            try
            {
                var photo = _context.Photos.FirstOrDefault(p => p.Title == title);
                _response.Data = photo;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost("PostPhoto")]
        public ResponseDto Post([FromBody] AtlasPhoto photo)
        {
            try
            {
                _context.Photos.Add(photo);
                _context.SaveChanges();

                _response.Data = photo;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }


        [HttpPut("PutPhoto")]
        public ResponseDto PutPhoto([FromBody] AtlasPhoto photo)
        {
            try
            {
                _context.Photos.Update(photo);
                _context.SaveChanges();

                _response.Data = photo;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpDelete("DeletePhoto/{id}")]
        public ResponseDto DeletePhoto(int id)
        {
            try
            {
                var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
                _context.Remove(photo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

    }
}

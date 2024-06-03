using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vendzoAPI.DTO;
using vendzoAPI.Interfaces;
using vendzoAPI.Models;

namespace vendzoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PromotionController : ControllerBase
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IMapper _mapper;

        public PromotionController(IPromotionRepository promotionRepository, IMapper mapper)
        {
            _promotionRepository = promotionRepository;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetPromotions() 
        {
            var promos = _mapper.Map<List<PromotionDTO>>(_promotionRepository.GetPromotions());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(promos);
        }

        [HttpGet("valid")]
        public IActionResult GetValidPromotions()
        {
            var promos = _mapper.Map<List<PromotionDTO>>(_promotionRepository.GetValidPromotions());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(promos);
        }

        [HttpGet("find/id={id}")]
        public IActionResult GetPromoById(string id)
        {
            if (string.IsNullOrEmpty(id) || !ModelState.IsValid)
                return BadRequest(ModelState);

            var promo = _mapper.Map<PromotionDTO>(_promotionRepository.GetById(id));

            if(promo == null)
                return NotFound();

            return Ok(promo);
        }
        [HttpGet("find/code={code}")]
        public IActionResult GetPromoByCode(string code)
        {
            if (string.IsNullOrEmpty(code) || !ModelState.IsValid)
                return BadRequest(ModelState);

            var promo = _mapper.Map<PromotionDTO>(_promotionRepository.GetByCode(code));

            if (promo == null)
                return NotFound();

            return Ok(promo);
        }

        [HttpGet("check/code={code}")]
        public IActionResult CheckPromoByCode(string code)
        {
            if (string.IsNullOrEmpty(code) || !ModelState.IsValid)
                return BadRequest(ModelState);

            var promo = _mapper.Map<PromotionDTO>(_promotionRepository.GetByCode(code));

            if (promo == null)
                return NotFound();

            if (_promotionRepository.CheckIsValid(code))
                return Ok(promo);

            return Ok("Expired");
        }

        [HttpPost("create")]
        public IActionResult CreatePromo([FromBody] PromotionDTO promotionDTO)
        {
            if (promotionDTO == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var promoMap = _mapper.Map<Promotion>(promotionDTO);
            promoMap.CreatedAt = DateTime.Now;

            if(!_promotionRepository.Add(promoMap))
            {
                ModelState.AddModelError("", "Something went wrong with adding the promotion :(");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        [HttpPut("update")]
        public IActionResult UpdatePromo (string promoId, [FromBody] PromotionDTO promotionDTO)
        {
            if(promotionDTO == null || !ModelState.IsValid || promoId != promotionDTO.Id)
                return BadRequest(ModelState);

            if (!_promotionRepository.PromotionExists(promoId))
                return NotFound();

            var promoMap = _promotionRepository.GetById(promoId);
            if (promoMap == null)   
                return NotFound();

            if (!string.IsNullOrEmpty(promotionDTO.Id))
            {
                promoMap.Id = promotionDTO.Id;
            }

            if (!string.IsNullOrEmpty(promotionDTO.PromoCode))
            {
                promoMap.PromoCode = promotionDTO.PromoCode;
            }

            if (promotionDTO.Amount.HasValue)
            {
                promoMap.Amount = promotionDTO.Amount.Value;
            }

            if (promotionDTO.Expires.HasValue)
            {
                promoMap.Expires = promotionDTO.Expires.Value;
            }

            if (!string.IsNullOrEmpty(promotionDTO.Type))
            {
                promoMap.Type = promotionDTO.Type;
            }

            if(!_promotionRepository.Update(promoMap))
            {
                ModelState.AddModelError("", "Something went wrong with updating the promo :(");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        [HttpDelete("delete/hard")]
        public IActionResult DeletePromo(string id)
        {
            if (string.IsNullOrEmpty(id) || _promotionRepository.PromotionExists(id)) 
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var promoToDelete = _promotionRepository.GetById(id);

            //TODO: Add relation validation (eg: baskets and orders with promo applied)

            if (promoToDelete == null || _promotionRepository.Delete(promoToDelete))
            {
                ModelState.AddModelError("", "Something went wrong with the deletion of the promo :(");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }
        [HttpDelete("delete/soft")]
        public IActionResult SoftDeletePromo(string id)
        {
            if (string.IsNullOrEmpty(id) || !_promotionRepository.PromotionExists(id))
                return NotFound();
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var promoToDelete = _promotionRepository.GetById(id);
            if(promoToDelete  == null)
                return NotFound();

            if(promoToDelete.IsDeleted)
            {
                ModelState.AddModelError("", "Promo is already soft deleted.");
                return StatusCode(500, ModelState);

            }
            promoToDelete.IsDeleted = true;

            if (!_promotionRepository.Update(promoToDelete))
            {
                ModelState.AddModelError("", "Something went wrong with soft deleting the promo :(");
                return StatusCode(500, ModelState);

            }
            return Ok("Success");
        }
    }
}

using System;
using System.Linq;
using AutoMapper;
using AspNetWebapiCore.Models;
using AspNetWebapiCore.Repositories;
using AspNetWebapiCore.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace FoodAPICore.Controller
{
    [Route("api/[controller]")]
    public class FoodController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IFoodRepository _foodRepository;

        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_foodRepository.GetAll().Select(x => Mapper.Map<FoodDto>(x)));
        }

        [HttpPost]
        public IActionResult Add([FromBody]FoodDto foodDto)
        {
            FoodItem newFoodItem = _foodRepository.Add(Mapper.Map<FoodItem>(foodDto));

            return CreatedAtRoute("GetSingleFood", new { id = newFoodItem.Id }, Mapper.Map<FoodDto>(newFoodItem));
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdate(int id, [FromBody] JsonPatchDocument<FoodDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            FoodItem existingEntity = _foodRepository.GetSingle(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            FoodDto foodItemDto = Mapper.Map<FoodDto>(existingEntity);
            patchDoc.ApplyTo(foodItemDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FoodItem updated = _foodRepository.Update(id, Mapper.Map<FoodItem>(foodItemDto));

            return Ok(Mapper.Map<FoodDto>(updated));
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetSingleFood")]
        public IActionResult Single(int id)
        {
            var foodItem = _foodRepository.GetSingle(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<FoodDto>(foodItem));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Remove(int id)
        {
            FoodItem foodItem = _foodRepository.GetSingle(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            _foodRepository.Delete(id);
            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody]FoodItem foodItem)
        {
            var foodItemToCheck = _foodRepository.GetSingle(id);

            if (foodItemToCheck == null)
            {
                return NotFound();
            }

            if (id != foodItemToCheck.Id)
            {
                return BadRequest("Ids do not match");
            }

            FoodItem update = _foodRepository.Update(id, foodItem);

            return Ok(Mapper.Map<FoodDto>(update));
        }
    }
}

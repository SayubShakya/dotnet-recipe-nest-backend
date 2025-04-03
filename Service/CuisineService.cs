using RecipeNest.Model;
using RecipeNest.Reponse;
using RecipeNest.Repository;
using RecipeNest.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeNest.Service
{
    public class CuisineService
    {
        private readonly ICuisineRepository _cuisineRepository;

        public CuisineService(ICuisineRepository cuisineRepository)
        {
            _cuisineRepository = cuisineRepository;
        }

        public List<CuisineResponse> GetAll()
        {
            List<Cuisine> cuisines = _cuisineRepository.GetAll();
            return cuisines.Select(cuisine => new CuisineResponse(
                cuisine.Id,
                cuisine.Name,
                cuisine.ImageUrl
            )).ToList();
        }

        public CuisineResponse? GetById(int id)
        {
            Cuisine? cuisine = _cuisineRepository.GetById(id); 
            if (cuisine == null)
            {
                return null;
            }

            return new CuisineResponse(
                cuisine.Id,
                cuisine.Name,
                cuisine.ImageUrl
            );
        }
        public CuisineResponse? GetByName(string name) 
        {
             if (string.IsNullOrWhiteSpace(name)) return null;

            Cuisine? cuisine = _cuisineRepository.GetByName(name);
            if (cuisine == null)
            {
                return null; 
            }

            return new CuisineResponse(
                cuisine.Id,
                cuisine.Name,
                cuisine.ImageUrl
            );
        }


        public bool Save(CreateCuisineRequest request)
        {
            Cuisine cuisine = new Cuisine
            {
                Name = request.Name,
                ImageUrl = request.ImageUrl
            };

            return _cuisineRepository.Save(cuisine); 
        }

        public bool Update(UpdateCuisineRequest request)
        {
            Cuisine? existingCuisine = _cuisineRepository.GetById(request.Id);
            if (existingCuisine == null)
            {
                throw new KeyNotFoundException($"Cuisine with ID {request.Id} not found.");
            }


            Cuisine cuisineToUpdate = new Cuisine
            {
                Id = request.Id, 
                Name = request.Name,
                ImageUrl = request.ImageUrl
            };

            return _cuisineRepository.Update(cuisineToUpdate); 
        }

        public bool DeleteById(int id)
        {
            return _cuisineRepository.DeleteById(id);
        }
    }
}
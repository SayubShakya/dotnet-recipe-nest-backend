using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RecipeNest.Consta;
using RecipeNest.Db;
using RecipeNest.Db.Query.Impl;
using RecipeNest.Model;
using RecipeNest.Repository;

namespace RecipeNest.Repository.Impl.Database
{
    public class RatingRepositoryDatabaseImpl : IRatingRepository
    {
        public Rating GetByUserAndRecipe(int userId, int recipeId)
        {
            try
            {
                return DatabaseConnector.QueryOne(IQueryConstant.IRating.GET_BY_ID, new RatingRowMapper(), userId, recipeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting rating by user/recipe (UserID: {userId}, RecipeID: {recipeId}): {ex.Message}");
                return null;
            }
        }

        public bool DeleteByUserAndRecipe(int userId, int recipeId)
        {
            try
            {
                DatabaseConnector.Update(IQueryConstant.IRating.DELETE_BY_ID, userId, recipeId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting rating by user/recipe (UserID: {userId}, RecipeID: {recipeId}): {ex.Message}");
                return false;
            }
        }
        

        public bool Save(Rating rating)
        {
            if (rating == null || rating.UserId == null || rating.RecipeId == null || rating.Score == null)
            {
                Console.WriteLine("Error saving rating: Required fields (UserId, RecipeId, Score) are missing.");
                return false;
            }

            try
            {
                var existing = GetByUserAndRecipe(rating.UserId.Value, rating.RecipeId.Value);
                if (existing != null)
                {
                    Console.WriteLine($"Rating already exists for UserID: {rating.UserId.Value}, RecipeID: {rating.RecipeId.Value}. Use update operation instead of save.");
                    return false;
                }

                RatingScore scoreToStore = rating.Score.Value;
                if (scoreToStore < RatingScore.Ten)
                {
                    scoreToStore++; 
                }
                byte scoreByte = (byte)scoreToStore;
                Console.WriteLine($"--- DEBUG: Saving Rating - Original: {rating.Score.Value}, Storing Enum: {scoreToStore}, Storing Byte: {scoreByte} ---");


                DatabaseConnector.Update(IQueryConstant.IRating.SAVE, rating.UserId.Value, rating.RecipeId.Value, scoreByte);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving rating (UserID: {rating.UserId}, RecipeID: {rating.RecipeId}): {ex.Message}");
                return false;
            }
        }

        public bool Update(Rating obj)
        {
            throw new NotImplementedException();
        }

        public Rating GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Rating> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
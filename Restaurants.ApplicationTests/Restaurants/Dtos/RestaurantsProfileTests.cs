﻿using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;

namespace Restaurants.Application.Restaurants.Dtos.Tests
{
	[TestClass()]
	public class RestaurantsProfileTests
	{

		private IMapper _mapper;

		public RestaurantsProfileTests()
		{
			var configuration = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<RestaurantsProfile>();
			});

			_mapper = configuration.CreateMapper();
		}

		[Fact()]
		public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
		{
			// arrange
			var restaurant = new Restaurant()
			{
				Id = 1,
				Name = "Test restaurant",
				Description = "Test Description",
				Categroy = "Test Category",
				HasDelivery = true,
				ContactEmail = "test@example.com",
				ContactNumber = "123456789",
				Address = new Address
				{
					City = "Test City",
					Street = "Test Street",
					PostalCode = "12-345"
				}
			};

			// act

			var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

			// assert 

			restaurantDto.Should().NotBeNull();
			restaurantDto.Id.Should().Be(restaurant.Id);
			restaurantDto.Name.Should().Be(restaurant.Name);
			restaurantDto.Description.Should().Be(restaurant.Description);
			restaurantDto.Categroy.Should().Be(restaurant.Categroy);
			restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
			restaurantDto.City.Should().Be(restaurant.Address.City);
			restaurantDto.Street.Should().Be(restaurant.Address.Street);
			restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);
		}

		[Fact()]
		public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapsCorrectly()
		{
			// arrange
			var command = new UpdateRestaurantCommand
			{
				Id = 1,
				Name = "Updated Restaurant",
				Description = "Updated Description",
				HasDelivery = false
			};

			// act

			var restaurant = _mapper.Map<Restaurant>(command);

			// assert 

			restaurant.Should().NotBeNull();
			restaurant.Id.Should().Be(command.Id);
			restaurant.Name.Should().Be(command.Name);
			restaurant.Description.Should().Be(command.Description);
			restaurant.HasDelivery.Should().Be(command.HasDelivery);
		}

		[Fact()]
		public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCorrectly()
		{
			// arrange
			var command = new CreateRestaurantCommand
			{
				Name = "Test Restaurant",
				Description = "Test Description",
				Categroy = "Test Category",
				HasDelivery = true,
				ContactEmail = "test@example.com",
				ContactNumber = "123456789",
				City = "Test City",
				Street = "Test Street",
				PostalCode = "12345"
			};

			// act

			var restaurant = _mapper.Map<Restaurant>(command);

			// assert 

			restaurant.Should().NotBeNull();
			restaurant.Name.Should().Be(command.Name);
			restaurant.Description.Should().Be(command.Description);
			restaurant.Categroy.Should().Be(command.Categroy);
			restaurant.HasDelivery.Should().Be(command.HasDelivery);
			restaurant.ContactEmail.Should().Be(command.ContactEmail);
			restaurant.ContactNumber.Should().Be(command.ContactNumber);
			restaurant.Address.Should().NotBeNull();
			restaurant.Address.City.Should().Be(command.City);
			restaurant.Address.Street.Should().Be(command.Street);
			restaurant.Address.PostalCode.Should().Be(command.PostalCode);
		}
	}
}
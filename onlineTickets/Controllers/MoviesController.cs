﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using onlineTickets.Data;
using onlineTickets.Data.Services;
using onlineTickets.Models;

namespace onlineTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);
            return View(allMovies);
        }

        //Get: Movies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync(id);
            return View(movieDetail);
        }

        // GET: Movies/Create

        public async Task<IActionResult> Create() {

            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "id", "FullName");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropdownsValues();


                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "id", "FullName");

                return View(movie);
            }

            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

    }
}

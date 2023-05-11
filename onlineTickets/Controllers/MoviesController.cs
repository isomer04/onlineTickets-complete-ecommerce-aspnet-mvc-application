﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using onlineTickets.Data;

namespace onlineTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allProducts = await _context.Movies.ToListAsync();
            return View();
        }
    }
}

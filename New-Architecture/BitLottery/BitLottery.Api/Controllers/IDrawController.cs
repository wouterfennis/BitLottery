﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BitLottery.Controllers
{
    /// <summary>
    /// Exposes methods to retrieve and create lottery draws
    /// </summary>
    public interface IDrawController
    {
        /// <summary>
        /// Creates a new draw
        /// </summary>
        Task<ActionResult> GenerateDraw();

        /// <summary>
        /// Retrieves a specific draw
        /// </summary>
        /// <param name="id">The id of the draw</param>
        string GetDraw(int id);
        
        /// <summary>
        /// Retrieves all draws
        /// </summary>
        IEnumerable<string> GetDraws();
    }
}
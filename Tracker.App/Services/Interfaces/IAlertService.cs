﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.App.Services.Interfaces;
public interface IAlertService
{
    Task DisplayAsync(string title, string message);
}

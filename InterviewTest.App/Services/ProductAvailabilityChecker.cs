﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InterviewTest.App.Models;

namespace InterviewTest.App.Services
{
    public class ProductAvailabilityChecker
    {
        public IProduct Product { get; }

        public ProductAvailabilityChecker(IProduct product)
        {
            Product = product;
        }

        public void CheckIfAvailable()
        {
            Thread.Sleep(5000);//Let us to check 
            Result = true;
        }
        public bool Result { get; private set; }
    }
}

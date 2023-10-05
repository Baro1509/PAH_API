﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service {
    public interface IImageService {
        public Task<string> StoreImageAsync(string fileName, Stream stream);
        public ProductImage GetMainImageByProductId(int productId);
    }
}

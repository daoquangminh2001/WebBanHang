﻿using WebHangHoa.DTO;

namespace WebHangHoa.Service
{
    public interface IHangHoaResponsitory
    {
        List<HangDTO> getall(string keyword, string sortBy, double? from, double? to, int page=1); 
    }
}

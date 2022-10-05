using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebHangHoa.Data;
using WebHangHoa.DTO;

namespace WebHangHoa.Service
{
    public class HangHoaResponsitory : IHangHoaResponsitory
    {
        private readonly HangHoaContext _context;
        private readonly IConfiguration _configuration;
        public static int page_size { get; set; } = 3;
        public HangHoaResponsitory(HangHoaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration= configuration;
        }
        public List<HangDTO> getall(string keyword,string sortBy, double? from, double? to, int page = 1)
        {
            var allProducts = _context.HangHoas.AsQueryable();//query giá trị trong bảng khác toList ở chỗ toList biên dịch xong mới lấy từ trong db còn asqueryable lấy giá trị trong lúc biên dịch 
                                                              //nếu keyword null thì get all giá trị
            #region Filtering
            if (!string.IsNullOrEmpty(keyword))
            {
                allProducts = allProducts.Where(h => h.TenHangHoa.Contains(keyword));//tìm những giá trị TenHangHoa có cụm từ có keyword
            }
            #endregion
            #region from to
            if(from.HasValue)   allProducts = allProducts.Where(h => h.DonGia>=from);
            if(to.HasValue) allProducts = allProducts.Where(h => h.DonGia<=to);
            #endregion
            #region Sorting
            allProducts = allProducts.OrderBy(h=>h.TenHangHoa);
            if(!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "tenHangHoa_desc": allProducts = allProducts.OrderByDescending(h => h.TenHangHoa);
                        break;
                    case "dongia": allProducts = allProducts.OrderBy(h => h.DonGia);
                        break;
                    case "dongia_desc": allProducts = allProducts.OrderByDescending(h => h.TenHangHoa);
                        break;
                }
            }
            #endregion
            #region Paging
            allProducts = allProducts.Skip((page - 1)*page_size).Take(page_size);
            #endregion
            // convert giá trị từ bảng HangHoa sang HangDTO
            var result = allProducts.Select(h => new HangDTO
                {
                    MaHangHoa = h.MaHangHoa,
                    TenHangHoa = h.TenHangHoa,
                    DonGia = h.DonGia,
                    TenLoai = h.LoaiHangHoa.TenLoai
                });
            
            return result.ToList();
        }

        public List<HangDTO> get_by_keyword(string? input)
        {
            var value = new List<HangDTO>();
            using(var connect = new SqlConnection(_configuration.GetSection("ConnectionStrings:Connect").Value))
            {
                if (connect.State == ConnectionState.Closed) connect.Open();
                var temp = new DynamicParameters();
                temp.Add("@input",input);
                var result = connect.Query<HangDTO>(
                    "[dbo].[Search_VietNamKeyword]",
                    temp,
                    commandType: CommandType.StoredProcedure
                    );
                value = result.ToList();
            }
            return value;
        }
    }
}
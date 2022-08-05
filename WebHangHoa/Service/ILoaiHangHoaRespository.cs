using WebHangHoa.DTO;

namespace WebHangHoa.Service
{
    public interface ILoaiHangHoaRespository
    {
        List<LoaiHangDTORespository> GetAll();
        LoaiHangDTORespository GetById(int id);
        LoaiHangDTORespository Add(LoaiHangHoaDTO Loai);
        void DeleteById(int id);
        void Update(LoaiHangDTORespository Loai);
    }
}

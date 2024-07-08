namespace MyAspNetCoreApp.Web.Models
{
    public class ArabaIslem
    {
        private static List<Araba> _arabalar = new List<Araba>()
        {
            new Araba{Id=1,Marka="Audi",Model="a8",fiyat="80.000$",CikisYili=2020},
            new Araba{Id=2,Marka="Togg",Model="TGX10",fiyat="70.250$",CikisYili=2022},
            new Araba{Id=3,Marka="Tesla",Model="X",fiyat="60.750$",CikisYili=2020}
        };

        public List<Araba> TumArabalar()=> _arabalar;

        public void Ekle(Araba arabaekle)=>_arabalar.Add(arabaekle);

        public void Sil(int id)
        {
            var ArabaVarMi = _arabalar.FirstOrDefault(x => x.Id == id);

            if (ArabaVarMi == null)
            {
                throw new Exception($"Bu ID({id}) değerine sahip araba bulunamadı.");
            }

            _arabalar.Remove(ArabaVarMi);
        }

        public void Guncelle(Araba GuncelleAraba)
        {
            var ArabaVarMi=_arabalar.FirstOrDefault(x=>x.Id == GuncelleAraba.Id);

            if(ArabaVarMi == null)
            {
                throw new Exception($"Bu ID({GuncelleAraba.Id}) değerine sahip araba bulunamadı ");
            }

            ArabaVarMi.Marka = GuncelleAraba.Marka;
            ArabaVarMi.Model=GuncelleAraba.Model;
            ArabaVarMi.fiyat = GuncelleAraba.fiyat;
            ArabaVarMi.CikisYili = GuncelleAraba.CikisYili;

            var index = _arabalar.FindIndex(x => x.Id == GuncelleAraba.Id);
            _arabalar[index]=ArabaVarMi;

        }
    }
}

# BsStoreApp - RESTful Web API

[![.NET](https://img.shields.io/badge/.NET-6.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/6.0)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-7.0.3-green.svg)](https://docs.microsoft.com/en-us/ef/)
[![Lisans](https://img.shields.io/badge/lisans-MIT-blue.svg)](LICENSE)

## 📖 Genel Bakış

BsStoreApp, .NET 6 ile geliştirilmiş kapsamlı bir RESTful Web API'sidir ve modern web API geliştirme uygulamalarını sergiler. Uygulama, tam CRUD işlemleri, sayfalama, filtreleme, önbellekleme, sürümleme ve HATEOAS (Hypermedia as the Engine of Application State) implementasyonu gibi gelişmiş özelliklerle birlikte bir kitap mağazası yönetim sistemi olarak hizmet verir.

## 🏗️ Mimari

Proje, endişelerin net ayrımı ile **Temiz Mimari** prensiplerini takip eder:

```
├── WebApi/              # Giriş noktası ve API konfigürasyonu
├── Presentation/        # Controller'lar ve API sunum katmanı
├── Services/           # İş mantığı ve servis implementasyonları
├── Repositories/       # Entity Framework ile veri erişim katmanı
└── Entities/          # Domain modelleri, DTO'lar ve paylaşılan sözleşmeler
```

### Temel Mimari Desenler

- **Repository Deseni**: Veri erişim mantığını soyutlar
- **Servis Katmanı Deseni**: İş mantığını kapsüller
- **Bağımlılık Enjeksiyonu**: Gevşek bağlantı ve test edilebilirliği destekler
- **DTO Deseni**: İç modelleri API sözleşmelerinden ayırır
- **CQRS benzeri yaklaşım**: Farklı işlemler için ayrı DTO'lar

## ✨ Özellikler

### Temel İşlevsellik
- 📚 **Kapsamlı Kitap Yönetimi**: Oluşturma, Okuma, Güncelleme, Silme işlemleri
- 🔍 **Gelişmiş Filtreleme**: Çeşitli kriterlere göre kitap filtreleme
- 📄 **Sayfalama**: Meta verilerle verimli veri alma
- 🔗 **HATEOAS**: Hipermedia odaklı API yanıtları
- 📝 **Kısmi Güncellemeler**: Verimli güncellemeler için JSON Patch desteği

### Teknik Özellikler
- 🚀 **API Sürümleme**: Çoklu API sürüm desteği
- 💾 **Yanıt Önbellekleme**: Önbellekleme stratejileri ile geliştirilmiş performans
- 📊 **Loglama**: NLog ile kapsamlı loglama
- 🔒 **CORS**: Cross-origin kaynak paylaşımı konfigürasyonu
- 📋 **Veri Doğrulama**: Action filter'lar ile güçlü giriş doğrulaması
- 🎯 **İçerik Müzakeresi**: Çoklu yanıt formatları (JSON, XML, CSV)
- 📈 **HTTP Önbellek Başlıkları**: İstemci tarafı önbellekleme optimizasyonu

## 🛠️ Teknoloji Yığını

### Backend Teknolojileri
- **.NET 6**: .NET'in en son LTS sürümü
- **ASP.NET Core Web API**: RESTful API framework'ü
- **Entity Framework Core 7.0.3**: Nesne-ilişkisel eşleme
- **SQL Server LocalDB**: Veritabanı motoru
- **AutoMapper 12.0.0**: Nesne-nesne eşleme
- **NLog 5.1.2**: Loglama framework'ü

### Temel NuGet Paketleri
- **Microsoft.EntityFrameworkCore**: Veri erişimi
- **Microsoft.AspNetCore.JsonPatch**: JSON Patch desteği
- **Microsoft.AspNetCore.Mvc.NewtonsoftJson**: JSON serileştirme
- **Microsoft.AspNetCore.Mvc.Versioning**: API sürümleme
- **System.Linq.Dynamic.Core**: Dinamik LINQ sorguları
- **Marvin.Cache.Headers**: HTTP önbellek başlıkları

## 🚀 Başlangıç

### Ön Koşullar

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) veya [Visual Studio Code](https://code.visualstudio.com/)

### Kurulum

1. **Repository'yi klonlayın**
   ```bash
   git clone https://github.com/yourusername/BsStoreApp.git
   cd BsStoreApp
   ```

2. **Bağımlılıkları geri yükleyin**
   ```bash
   dotnet restore
   ```

3. **Veritabanı bağlantı dizesini güncelleyin** (gerekirse)
   
   `WebApi/appsettings.json` dosyasını düzenleyin:
   ```json
   {
     "ConnectionStrings": {
       "sqlConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=bsStoreApp;Integrated Security=true;"
     }
   }
   ```

4. **Veritabanı migration'larını çalıştırın**
   ```bash
   dotnet ef database update --project WebApi
   ```

5. **Uygulamayı çalıştırın**
   ```bash
   dotnet run --project WebApi
   ```

6. **API'ye erişin**
   - API Temel URL: `https://localhost:7001` veya `http://localhost:5001`
   - Swagger UI: `https://localhost:7001/swagger`

## 📚 API Dokümantasyonu

### Temel Endpoint'ler

| Method | Endpoint | Açıklama |
|--------|----------|----------|
| GET | `/api/books` | Sayfalama ile tüm kitapları getir |
| GET | `/api/books/{id}` | Belirli bir kitabı getir |
| POST | `/api/books` | Yeni kitap oluştur |
| PUT | `/api/books/{id}` | Kitabı güncelle |
| PATCH | `/api/books/{id}` | Kitabı kısmen güncelle |
| DELETE | `/api/books/{id}` | Kitabı sil |
| OPTIONS | `/api/books` | İzin verilen HTTP metodlarını getir |
| HEAD | `/api/books` | Gövde olmadan başlıkları getir |

### Sorgu Parametreleri

- **Sayfalama**: `pageNumber`, `pageSize`
- **Filtreleme**: `minPrice`, `maxPrice`, `searchTerm`
- **Sıralama**: `orderBy`
- **Alan Seçimi**: `fields`

### Örnek İstekler

#### Sayfalama ile Kitapları Getirme
```http
GET /api/books?pageNumber=1&pageSize=10&minPrice=10&maxPrice=100
```

#### Yeni Kitap Oluşturma
```http
POST /api/books
Content-Type: application/json

{
  "title": "Temiz Kod",
  "price": 45.99
}
```

#### JSON Patch ile Kısmi Güncelleme
```http
PATCH /api/books/1
Content-Type: application/json-patch+json

[
  {
    "op": "replace",
    "path": "/price",
    "value": 39.99
  }
]
```

## 🔧 Konfigürasyon

### Loglama Konfigürasyonu

Loglama `nlog.config` ile konfigüre edilir:
- Log dosyaları `./logs/` dizininde saklanır
- İç loglar `./internal_logs/` dizininde saklanır
- Konfigüre edilebilir log seviyeleri ve hedefleri

### Önbellekleme Konfigürasyonu

- **Yanıt Önbellekleme**: GET istekleri için 5 dakikalık önbellek profili
- **HTTP Önbellek Başlıkları**: Public önbellek konumu ile 80 saniyelik max-age
- **Önbellek Doğrulama**: Konfigüre edilebilir yeniden doğrulama ayarları

### CORS Konfigürasyonu

- Tüm origin'lere, metodlara ve başlıklara izin verir
- Sayfalama meta verileri için `X-Pagination` başlığını açığa çıkarır

## 🧪 Test Etme

### Swagger UI Kullanarak
1. `https://localhost:7001/swagger` adresine gidin
2. Mevcut tüm endpoint'leri keşfedin ve test edin
3. İstek/yanıt şemalarını görüntüleyin

### Postman Kullanarak
1. API koleksiyonunu içe aktarın (varsa)
2. Temel URL'yi `https://localhost:7001` olarak ayarlayın
3. Farklı parametrelerle çeşitli endpoint'leri test edin

## 📁 Proje Yapısı

```
BsStoreApp/
├── Entities/
│   ├── DTOs/                    # Veri Transfer Nesneleri
│   ├── Models/                  # Domain Modelleri
│   ├── Exceptions/              # Özel İstisnalar
│   ├── RequestFeatures/         # Sayfalama ve Filtreleme
│   └── LinkModels/              # HATEOAS Link Modelleri
├── Repositories/
│   ├── Contracts/               # Repository Arayüzleri
│   └── EFCore/                  # Entity Framework Implementasyonları
├── Services/
│   ├── Contracts/               # Servis Arayüzleri
│   └── *.cs                     # Servis Implementasyonları
├── Presentation/
│   ├── Controllers/             # API Controller'ları
│   └── ActionFilters/           # Özel Action Filter'lar
└── WebApi/
    ├── Extensions/              # Servis Uzantıları
    ├── Utilities/               # Yardımcı Sınıflar
    └── Program.cs               # Uygulama Giriş Noktası
```

## 🤝 Katkıda Bulunma

1. Repository'yi fork edin
2. Özellik dalı oluşturun (`git checkout -b feature/harika-ozellik`)
3. Değişikliklerinizi commit edin (`git commit -m 'Harika özellik ekle'`)
4. Dalı push edin (`git push origin feature/harika-ozellik`)
5. Pull Request açın

## 📄 Lisans

Bu proje MIT Lisansı altında lisanslanmıştır - detaylar için [LICENSE](LICENSE) dosyasına bakın.

## 👨‍💻 Yazar

**Ahmet Coşkun Kızılkaya**

## 🙏 Teşekkürler

- Robert C. Martin'in Temiz Mimari prensipleri
- RESTful API tasarım en iyi uygulamaları
- ASP.NET Core topluluğu ve dokümantasyonu
- Entity Framework Core ekibi

---

⭐ Bu projeyi faydalı bulduysanız, lütfen yıldız verin!
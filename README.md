# 📝 Makale Yazari MVC

> Kullanıcıların kendi makalelerini oluşturup yönetebildiği, kategorilerle zenginleştirilmiş ASP.NET Core MVC uygulaması.

---

## 🚀 Proje Hakkında

Bu proje, kullanıcıların **giriş yaptıktan sonra** makale yazıp, makalelerini birden fazla kategoriye atayabildiği, sadece kendi makaleleri üzerinde **CRUD (Oluştur, Oku, Güncelle, Sil)** işlemi yapabildiği bir web uygulamasıdır.

- Kategori verileri önceden tanımlı (seed data) olarak gelir.  
- Ana sayfada herkes tüm makaleleri görebilir.  
- Bootstrap ile kullanıcı dostu, sade ve temiz arayüz tasarlandı.

---

## 🛠️ Teknolojiler & Araçlar

| Teknoloji          | Kullanım Alanı                  |
|--------------------|--------------------------------|
| 🖥️ ASP.NET Core MVC| Web uygulama çatısı            |
| 🗄️ EF Core         | Veritabanı erişim & ilişki yönetimi |
| 🔐 ASP.NET Identity | Kullanıcı yönetimi & kimlik doğrulama |
| 🎨 Bootstrap       | Responsive ve estetik tasarım  |
| 🛢️ SQL Server      | Veritabanı                     |

---

## 🧩 Özellikler

- ✔️ Kullanıcı kaydı ve giriş (Identity)  
- ✔️ Çoklu kategori seçimi (Checkbox list)  
- ✔️ Kullanıcıya özel makale yönetimi (Sadece kendi makalelerini düzenler)  
- ✔️ Makale başlığı, içeriği ve kategorilerinin yönetimi  
- ✔️ Ana sayfada tüm makalelerin listelenmesi  
- ✔️ Kategori verileri seed data ile otomatik yüklenir (Örnek: Teknoloji, Eğitim, Sağlık)

---

## ⚙️ Kurulum & Çalıştırma

```bash
# Projeyi klonlayın
git clone https://github.com/kullaniciadi/MakaleYazariMVC.git

# Proje dizinine girin
cd MakaleYazariMVC

# Bağlantı ayarlarını yapın (appsettings.json)

# Veritabanını oluşturun
dotnet ef database update

# Uygulamayı başlatın
dotnet run

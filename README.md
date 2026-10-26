# AutoSettlementBuilder (Akıllı Fief İnşaatı)

**AutoSettlementBuilder**, *Mount & Blade II: Bannerlord* için geliştirilmiş akıllı bir yerleşim yönetim modudur.

Oyuncunun yönettiği kale ve şehirlerde, oyuncu elle bir inşaat emri vermediğinde (inşaat kuyruğu boşaldığında), yerleşimin anlık ihtiyaç ve kriz durumlarını (Sadakat, Yiyecek, Güvenlik, İnşaat Hızı, Surlar, Ekonomi vb.) analiz ederek en uygun binayı otomatik olarak inşaat sırasına koyar.

---

## 🌟 Özellikler

- **Oyuncunun Emirlerine Saygı:** Oyuncu manuel olarak bir bina sıraya koymuşsa mod sıraya kesinlikle müdahale etmez. Yalnızca kuyruk boşaldığında devreye girer.
- **Akıllı İhtiyaç Analizi Motoru:** Binaları sabit isimlerle değil, Bannerlord'un kendi `BuildingEffectEnum` motor etkileri üzerinden dinamik olarak puanlar.
  - **Sadakat Krizi (Loyalty < 30):** İsyan riskini önlemek için Forum/Adliye binaları ve *Festival & Oyunlar* günlük projesi devreye girer.
  - **Gıda Kıtlığı (FoodStocks < 20 veya negatif gıda artışı):** Garnizonun açlıktan kırılmasını durdurmak için Ambar/Sulama binaları ve *Sulama* projesi seçilir.
  - **Düşük Güvenlik (Security < 40):** Nöbetçi kulübesi ve muhafız ofisi geliştirilir.
  - **İnşaat Hızı Kaldıracı:** Temel krizler giderildiğinde, sonraki tüm inşaatları hızlandıracak olan Taşçı/Atölye binasına öncelik verilir.
  - **Sur & Savunma:** Krallık savaştayken sur geliştirmeleri desteklenir.
- **Akıllı Günlük Proje Yönetimi:** Boşta kalan yerleşimlerde kriz durumuna göre en verimli varsayılan proje (*Festival ve Oyunlar* ya da *Sulama*) otomatik seçilir.
- **Sıfır Dış Bağımlılık & Save Güvenliği:** Harmony, MCM veya ek kütüphane gerektirmez. Save dosyasına yabancı veri kaydetmez (`SyncData` vanilla kalır); mevcut save'lerde güvenle kullanılabilir ve istendiğinde save bozulmadan kaldırılabilir.
- **Opsiyonel İnşaat Fonu Desteği:** İsteğe bağlı olarak oyuncunun parası belirlenen limitin üzerindeyse şehre otomatik 10.000 Dinar inşaat fonu (`BoostBuildingProcess`) aktarabilir.
- **Çoklu Dil Desteği:** Türkçe (`tr`) ve İngilizce (`en`) oyun içi bildirim mesajları.

---

## 🛠️ Kurulum

1. Bu depoyu indirin veya Release paketini edinin.
2. `AutoSettlementBuilder` klasörünü Bannerlord kurulumunuzdaki `Modules` klasörüne kopyalayın:
   ```
   Mount & Blade II Bannerlord\Modules\AutoSettlementBuilder\
   ├── SubModule.xml
   ├── ModuleData\
   │   └── config.json
   └── bin\
       └── Win64_Shipping_Client\
           └── AutoSettlementBuilder.dll
   ```
3. Bannerlord Launcher'ını açın, **Mods** sekmesinden **Auto Settlement Builder**'ı aktif edin.

---

## ⚙️ Yapılandırma (`config.json`)

Mod ayarları `ModuleData/config.json` dosyasından düzenlenebilir:

```json
{
  "Enabled": true,
  "NotifyOnAutoQueue": true,
  "ManageDailyProjects": true,
  "LoyaltyCrisisThreshold": 30.0,
  "FoodCrisisThreshold": 20.0,
  "SecurityCrisisThreshold": 40.0,
  "PrioritizeMasonFirst": true,
  "AutoBoostConstructionFund": false,
  "AutoBoostMinPlayerGold": 150000,
  "AutoBoostFundAmount": 10000,
  "Language": "tr"
}
```

---

## 💻 Geliştirme ve Derleme

Projeyi derlemek için .NET SDK (netstandard2.0 destekli) gereklidir:

```powershell
dotnet build AutoSettlementBuilder.csproj -c Release
```

Derleme çıktısı otomatik olarak Bannerlord'un `Modules/AutoSettlementBuilder` dizinine kopyalanır.

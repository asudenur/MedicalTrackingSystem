WEB TEKNOLOJİLERİ FİNAL PROJESİ RAPORU
TIBBİ TAKİP SİSTEMİ
Bu proje, bir hastane yönetim sistemini simüle etmek amacıyla geliştirilmiş bir web uygulamasıdır. Uygulama, kullanıcıların doktor ve hasta bilgilerini yönetmelerine, giriş yapmalarına ve kaydolmalarına olanak tanıyan bir platform sağlar. Ayrıca, uygulamanın Docker ile kapsüllenmesi, yazılımın taşınabilirliğini artırarak herhangi bir ortamda kolayca çalıştırılmasını mümkün kılar.
Proje Özellikleri
1. Giriş ve Kayıt
Uygulama, kullanıcıların sisteme giriş yapabilmesi veya kaydolabilmesi için bir kullanıcı doğrulama mekanizması içerir. Kullanıcı türleri aşağıdaki şekilde ayrılmıştır:
•	Admin:
  o	Tüm kullanıcıları yönetebilir ve görüntüleyebilir.
  o	Doktor ve hasta ekleme-silme gibi yetkilere sahiptir.
  o	Örnek Admin Kullanıcı Bilgileri:
  	Kullanıcı Adı: admin
  	Şifre: admin123
•	Doctor:
  o	Kendi hastalarını yönetebilir ve görüntüleyebilir.
  o	Hastalarına özel hastalık bilgisi, ilaç reçetesi ve tedavi kayıtları ekleyebilir.
•	Patient:
  o	Kendi tedavi geçmişini ve reçetelerini görebilir.
  o	Hastane doktorlarından online randevu alabilir.
2. Veritabanı Entegrasyonu
Uygulama, tüm kullanıcı bilgilerini ve ilişkili verileri saklamak için SQL Server veritabanını kullanmaktadır. Veritabanı bağlantıları, modern ORM (Object Relational Mapping) teknolojilerinden biri olan Entity Framework Core kullanılarak gerçekleştirilmiştir. Bu sayede, veritabanı işlemleri basitleştirilmiş ve uygulama performansı optimize edilmiştir.
3. Session Yönetimi
Kullanıcıların giriş yaptıktan sonra oturum bilgileri, Session mekanizması kullanılarak saklanır. Oturum yönetimi sayesinde:
  •	Kullanıcı kimlik doğrulama bilgileri korunur.
  •	Kullanıcının türüne göre (Admin, Doctor, Patient) özel yetkilendirme ve yönlendirme yapılır.
  •	Güvenlik önlemleri artırılmıştır.
4. API Destekli Yapı
Uygulama, API Controller yapılarına sahiptir. Bu controller'lar, kullanıcı bilgileri ve işlem yönetimi gibi işlevleri API tabanlı bir yaklaşımla sunar.
•	Örnek API İşlemleri:
  o	Yeni kullanıcıların eklenmesi.
  o	Kullanıcı verilerinin güncellenmesi veya silinmesi.
  o	Doktorların hasta kayıtlarına erişimi ve veri ekleyebilmesi.
5. Docker Entegrasyonu
Uygulama, Docker container içinde çalışacak şekilde yapılandırılmıştır. Projede bulunan Dockerfile, uygulamanın tüm bağımlılıklarıyla birlikte bir container'a paketlenmesini sağlar. Docker sayesinde:
  •	Uygulama taşınabilir hale gelir ve herhangi bir ortamda kolayca çalıştırılabilir.
  •	Geliştirme, test ve üretim ortamları arasında tutarlılık sağlanır.
Teknolojiler
  •	ASP.NET Core MVC: Uygulama, modern web geliştirme standartlarını karşılayan ASP.NET Core MVC çatısı kullanılarak geliştirilmiştir. Bu yapı, Model-View-Controller (MVC) mimarisini esas alır ve temiz kod yazımı ile güçlü bir yapı sunar.
  •	Entity Framework Core: Uygulamanın veritabanı işlemleri için kullanılan bu ORM aracı, SQL Server veritabanına erişimi kolaylaştırır ve performansı artırır.
  •	SQL Server: Uygulamanın tüm veri yönetimi ve saklama işlemleri, güvenilir ve güçlü bir veritabanı olan SQL Server kullanılarak gerçekleştirilmiştir.
  •	Docker: Uygulamanın platform bağımsız bir şekilde çalışabilmesi ve taşınabilir olması için kullanılmıştır. Docker container'ları sayesinde uygulama, geliştirildiği ortamdan bağımsız olarak her yerde aynı şekilde çalışabilir.
Projenin Teknik Detayları
Veritabanı Yapısı
Veritabanı, hastane yönetim sisteminin ihtiyaçlarını karşılamak üzere aşağıdaki ana tabloları içerir:
1.	Admin: Kullanıcı bilgilerini (ID, Ad, Soyad, Kullanıcı Türü, Şifre vb.) saklar.
2.	Doctors: Doktorlara özel bilgileri (ID, Uzmanlık Alanı vb.) içerir.
3.	Patients: Hastaların bilgilerini (ID, Doğum Tarihi, İlaç Bilgisi, Hastalık Bilgisi, Cinsiyet vb.) içerir.
4.	Branches: Doktorların branşları tutulur.(Kardiyoloji, Dermatoloji vb.)
________________________________________
Sonuç ve Değerlendirme
Tıbbi Takip Sistemi, bir hastane yönetim sisteminin temel işlevlerini simüle etmek için tasarlanmış, kullanıcı dostu bir web uygulamasıdır. Proje, modern teknolojilerle geliştirilmiş ve taşınabilirliği artırmak için Docker entegrasyonu ile desteklenmiştir. Bu proje, hem kullanıcı dostu bir arayüz hem de güçlü bir altyapı ile, hastane yönetimi gibi karmaşık sistemlerin nasıl optimize edilebileceğini göstermektedir.
Aşağıda projenin kendisine ait ekran görüntüleri bulunmaktadır:

 ![image](https://github.com/user-attachments/assets/d104a173-a142-499f-a171-11f6471fe1c0)
-Bu login sayfasına ait bir ekran görüntüsüdür. Kişi TC kimlik numarasıyla ve şifresiyle giriş yapabilir.

 ![image](https://github.com/user-attachments/assets/cc32a670-1e95-4679-862b-8b0d85624be6)
-Bu register sayfasına ait ekran görüntüsüdür. User Type kısmında Patient ve Doctor kısmı bulunmaktadır. Patient seçerse hastanın yaşı sorulmaktadır. Doctor seçilirse doktorun branşı sorulmaktadır.
 
 ![image](https://github.com/user-attachments/assets/8470e388-ad0d-4c05-a0c3-451188245cab)
-Bu şekilde branş seçebilir kayıt olan kişi.
 
 ![image](https://github.com/user-attachments/assets/edd409a4-7e41-47e7-bba9-3b2a746a4a77)
-Admin kısmıdır. Admin hem doktorları hem de hastaları görünteleme ve yönetme imkanına sahiptir. Manage Doctors seçilirse aşağıdaki gibidir;
 
 ![image](https://github.com/user-attachments/assets/a44a10ff-74ef-4cef-845a-467937bcb383)
-Bu ekranda yeni doktor eklenebilir. Ve doktorlar silinebilir. 

 Manage Patients seçilseydi eğer;
 ![image](https://github.com/user-attachments/assets/e5c23214-4b20-427a-a8ff-278967a7d8da)
-Yeni hasta eklenebilir ve eski hastalara silinebilir.


![image](https://github.com/user-attachments/assets/31154464-b97e-44ac-87c9-a92c3fcb12a1) 
-Eğer bir doktor giriş yaptıysa doktorun sayfası bu şekildedir. Solda kendi kişisel bilgileri ve sağda da hastalarının bilgileri bulunmaktadır. View Patients dediğimizde ise;

 ![image](https://github.com/user-attachments/assets/ebe1db34-fc42-426d-899d-c47fea2e3821)
-Bu şekilde doktorun hastaları hakkındaki bilgilere ulaşabildiğini görmekteyiz. Ve doktor eğer isterse Update butonundan hastanın Disease, MedicalRecords ve Medicine kısmına yeni şeyler ekleyebilir ve eskisini silebilme yetkisine sahiptir. Update kısmı da aşağıdaki gibidir;

![image](https://github.com/user-attachments/assets/15a4ff18-18a0-4c2a-a400-e302e96179c3)
-Bu şekilde hastanın bilgilerini güncelleyebiliriz.

 ![image](https://github.com/user-attachments/assets/cb45adf3-a745-4da6-acdf-926dd21974e9)
-Bu ekran görüntüsü de bir hastaya aittir. Bu şekilde kendi bilgilerini görebilir. Ve yukarıdaki Find Doctor’a tıklayınca da aşağıdaki şekilde gözükmektedir;

 ![image](https://github.com/user-attachments/assets/6f879c5b-1f1f-4b6b-b748-80cad35e3c2a)
-Bu şekilde hastaneye ait doktorların bilgisi gelmektedir. İstenilen doktorun üzerindeki Book Appointment tuşuna basarak doktordan randevu alınır ve hasta doktorun sayfasına düşer.

 ![image](https://github.com/user-attachments/assets/673f80fb-6e2a-48d4-822d-3d494a70584f)
 Bu şekilde görülmektedir. 
 
![image](https://github.com/user-attachments/assets/6bdda078-c92f-4545-a4c5-ba331eb8887e)


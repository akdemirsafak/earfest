
using System.ComponentModel.DataAnnotations.Schema;
using static MassTransit.ValidationResultExtensions;

namespace earfest.API.Domain.Entities;

public class Order : AbstractEntity, IAuditableEntity
{
    public string PlanId { get; set; }
    public string PlanName { get; set; }
    public string BuyerId { get; set; }

    public string CardNumber { 
        get 
        {
            return MaskedCardNumber;
        } 
        set 
        {
            string lastFourDigits = value.Substring(value.Length - 4);
            string maskedPart= new string('*', value.Length-4);
            
            MaskedCardNumber = maskedPart + lastFourDigits;
        }
    }
    [NotMapped] // Veritabanına yansıtılmayacak.
    private string MaskedCardNumber { get; set; }

    public string HolderName
    {
        get
        {
            return MaskedHolderName;
        }
        set
        {
            string[] names = value.Split(' ');

            string name = names[0];
            string lastName = names[1];
            name = name.Length > 2
            ? name.Substring(0, 2) + new string('*', name.Length - 2)
            : name; // Eğer string 2 karakterden kısaysa orijinal string döndürülür.
            lastName = lastName.Length > 2
            ? lastName.Substring(0, 2) + new string('*', lastName.Length - 2)
            : lastName; // Eğer string 2 karakterden kısaysa orijinal string döndürülür.
            MaskedHolderName = name + " " + lastName;
        }
    }
    [NotMapped] // Veritabanına yansıtılmayacak.
    private string MaskedHolderName { get; set; }

    //public string Token { get; set; }//Ödeme işlemi başarılı olursa token oluşturulacak ve bu token ile plana erişim sağlanacak.
    //Çoğu ödeme sağlayıcısı(örneğin Iyzico, Stripe, PayPal), "tokenization" adı verilen bir yöntemle kart bilgilerini kendileri saklar ve yalnızca bir token döner.
    //Bu token, ödeme işlemleri için kullanılabilir ve hassas kart bilgilerini barındırmaz.
    public decimal Price { get; set; }
    public DateTime CreatedAt { get ; set ; }
    public string? CreatedBy { get ; set ; }
    public DateTime? UpdatedAt { get ; set ; }
    public string? UpdatedBy { get ; set ; }
    public bool IsDeleted { get ; set ; } // Sipariş iptal edildiyse.
    public DateTime? DeletedAt { get ; set ; }
    public string? DeletedBy { get ; set ; }
}

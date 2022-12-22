using NpgsqlTypes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIS8_2.MVVM.Model;

public partial class Tuser
{
    public int Id { get; set; }

    public int? IdOrg { get; set; }

    public int? IdOmsu { get; set; }

    public string Login { get; set; }

    public string Passwordhash { get; set; }
    [Column("role")]
    public role_type Role { get; set; }

    public virtual Omsu IdOmsuNavigation { get; set; }

    public virtual Organisation IdOrgNavigation { get; set; }

    public enum role_type
    {
        [Description("Оператор по отлову"), PgName("Оператор по отлову")]
        operOtl,
        [Description("Куратор ВетСлужбы"), PgName("Куратор ВетСлужбы")]
        kurVet,
        [Description("Куратор ОМСУ"), PgName("Куратор ОМСУ")]
        kurOMSU,
        [Description("Куратор по отлову"), PgName("Куратор по отлову")]
        kurOtl,
        [Description("Оператор ВетСлужбы"), PgName("Оператор ВетСлужбы")]
        operVet,
        [Description("Оператор ОМСУ"), PgName("Оператор ОМСУ")]
        operOMSU,
        [Description("Подписант ВетСлужбы"), PgName("Подписант ВетСлужбы")]
        podpisVet,
        [Description("Подписант ОМСУ"), PgName("Подписант ОМСУ")]
        podpisOMSU,
        [Description("Подписант по отлову"), PgName("Подписант по отлову")]
        podpisOtl
    }
}

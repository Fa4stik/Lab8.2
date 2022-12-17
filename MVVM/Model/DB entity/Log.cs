using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIS8_2.MVVM.Model;

public partial class Log
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int IdUser { get; set; }

    public int IdCard { get; set; }
    [Column("operation")]
    public operation Operation { get; set; }

    public virtual Card IdCardNavigation { get; set; }

    public virtual Tuser IdUserNavigation { get; set; }

    public enum operation
    {
        [Description("Удаление карточки из реестра"), PgName("Удаление карточки из реестра")]
        delCardReestr,
        [Description("Добавление карточки в реестр"), PgName("Добавление карточки в реестр")]
        addCardReestr,
        [Description("Изменение карточки"), PgName("Изменение карточки")]
        editCard,
        [Description("Удаление файла"), PgName("Удаление файла")]
        delFile
    }
}

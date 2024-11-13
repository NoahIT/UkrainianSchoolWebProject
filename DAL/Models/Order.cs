using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Order : Entity
    {
        public int SubscriptionTypeId { get; set; }
        public virtual SubscriptionType SubscriptionType { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public GradeClass GradeClass { get; set; }

        public bool isValid { get; set; }
        public StatusCode Status { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual List<OrderSubject> Subjects { get; set; }
    }

    public enum StatusCode
    {
        InProcessing, // Заказ все еще находится в процессе обработки платежным шлюзом
        WaitingAuthComplete, // Заказ ожидает подтверждения списания средств (Settle)
        Approved, // Заказ успешно оплачен клиентом, средства списаны с карты
        Pending, // На проверке Antifraud
        Expired, // Истек срок оплаты
        Refunded, // Возвращение средств (Возвращение/разблокировка денег)
        Voided, // Разблокировка средств (вместе с Refunded)
        Declined, // Отклоненный заказ
        RefundInProcessing, // Возврат в обработке

        Failed, // Транзакция не удалась (может быть аналогично Declined)
        Started // Начало процесса транзакции
    }

    public enum GradeClass
    {
        Class10 = 10,
        Class11 = 11
    }
}

namespace Smartwyre.DeveloperTest.Types;

//TODO ran out of time here; in production code, I'd remove this file altogether and have the Product class hold a List<Incentive>. 
//The bit shift is a nice trick but you end up with two enums to maintain instead of one. In non-SQL databases, holding a List<T> isn't
//a problem; in SQL, you can just have a mapping table or - if you don't need lookup, ever - serialize/deserialize into/from JSON.
public enum SupportedIncentiveType
{
    FixedRateRebate = 1 << 0,
    AmountPerUom = 1 << 1,
    FixedCashAmount = 1 << 2,
}

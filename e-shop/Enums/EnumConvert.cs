namespace e_shop.Enums
{
    public static class EnumConvert
    {
        public static string GetShippingMethod(ShippingMethod method)
        {
            return method == ShippingMethod.CzechPost 
                ? "Česká Pošta" 
                : method == ShippingMethod.Zasilkovna 
                ? "Zásilkovna" 
                : "Na pobočce";
        }

        public static string GetShippingMethod(int method)
        {
            return GetShippingMethod((ShippingMethod)method);
        }
    }
}

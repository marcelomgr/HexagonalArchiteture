namespace Domain
{
    public static class Utils
    {
        public static bool ValidateCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if (cpf.Length != 11 || !long.TryParse(cpf, out long numericCpf))
                return false;

            long sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += numericCpf / (long)Math.Pow(10, 8 - i) % 10 * (9 - i);
            }

            long remainder = (sum % 11) % 10;
            if (numericCpf / 10 % 10 != remainder)
                return false;

            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += numericCpf / (long)Math.Pow(10, 9 - i) % 10 * (10 - i);
            }

            remainder = (sum % 11) % 10;
            return numericCpf % 10 == remainder;
        }
    }
}
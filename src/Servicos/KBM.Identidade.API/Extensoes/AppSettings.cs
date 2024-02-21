﻿namespace KBM.Identidade.API.Extensoes;

public class AppSettings
{
    public string Secret { get; set; } = string.Empty;
    public int ExpiracaoHoras { get; set; }
    public string Emissor { get; set; } = string.Empty;
    public string ValidoEm { get; set; } = string.Empty;
}

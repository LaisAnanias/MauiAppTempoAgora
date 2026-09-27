using System.Net.Http;
using Newtonsoft.Json;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();
        private const string apiKey = "e337fa87755517b10998a84e7b91e8cf";

        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnBuscar_Clicked(object sender, EventArgs e)
        {
            string cidade = entryCidade.Text;

            if (string.IsNullOrWhiteSpace(cidade))
            {
                await DisplayAlert("Atenção", "Digite o nome de uma cidade.", "OK");
                return;
            }

            await GetPrevisao(cidade);
        }

        private async Task GetPrevisao(string cidade)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={apiKey}&units=metric&lang=pt_br";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var tempo = JsonConvert.DeserializeObject<Tempo>(json);

                    lblResultado.Text = $"Cidade: {tempo.name}\n" +
                                         $"Temperatura: {tempo.main.temp}°C\n" +
                                         $"Descrição: {tempo.weather[0].description}\n" +
                                         $"Vento: {tempo.wind.speed} m/s\n" +
                                         $"Visibilidade: {tempo.visibility} m";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await DisplayAlert("Erro", "Cidade não encontrada. Verifique o nome digitado.", "OK");
                }
                else
                {
                    await DisplayAlert("Erro", $"Ocorreu um erro: {response.StatusCode}", "OK");
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Sem conexão", "Verifique sua conexão com a internet e tente novamente.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro inesperado", ex.Message, "OK");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CRUDConsultaAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string URL = "";


        private async void ConsultarPessoaFisicaPorId(int pIdPessoa)
        {
            URL = txtURLAPI.Text + "/" + pIdPessoa.ToString();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(URL);

                if (response.IsSuccessStatusCode)
                {
                    var PessoaJsonString = await response.Content.ReadAsStringAsync();

                    PessoaFisica pessoa = JsonConvert.DeserializeObject<PessoaFisica>(PessoaJsonString);

                    txtNome.Text = pessoa.Nome;
                    txtDtNascimento.Text = pessoa.DataNascimento.ToString("dd/MM/yyyy");
                    txtValorRenda.Text = pessoa.ValorRenda.ToString();
                    txtCPF.Text = pessoa.CPF;
                }
                else
                {
                    MessageBox.Show("Erro: " + response.StatusCode);
                }
            }
        }

        private async void IncluirPessoaFisica()
        {
            URL = txtURLAPI.Text;

            using (var client = new HttpClient())
            {
                var PessoaFisica = new PessoaFisica()
                {
                    Id = Convert.ToInt32(txtID.Text),
                    Nome = txtNome.Text,
                    DataNascimento = Convert.ToDateTime(txtDtNascimento.Text),
                    ValorRenda = Convert.ToDouble(txtValorRenda.Text),
                    CPF = txtCPF.Text
                };

                HttpResponseMessage result = await client.PutAsJsonAsync(URL + "/", PessoaFisica);

                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Dados da pessoa " + txtNome.Text + " e código " + txtID.Text + " cadastrados com sucesso.");
                }
                else
                {
                    MessageBox.Show("Erro: " + result.StatusCode);
                }
            }
        }

        private async void AtualizarPessoa()
        {
            URL = txtURLAPI.Text;

            PessoaFisica pessoa = new PessoaFisica()
            {
                Id = Convert.ToInt32(txtID.Text),
                Nome = txtNome.Text,
                DataNascimento = Convert.ToDateTime(txtDtNascimento.Text),
                ValorRenda = Convert.ToDouble(txtValorRenda.Text),
                CPF = txtCPF.Text
            };

            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(URL + "/" + pessoa.Id, pessoa);

                if (responseMessage.IsSuccessStatusCode)
                {
                    MessageBox.Show("Dados da pessoa " + txtNome.Text + " e código " + txtID.Text + " atualizados com sucesso.");
                }
                else
                {
                    MessageBox.Show("Erro: " + responseMessage.StatusCode);
                }
            }
        }

        private async void ExcluirPessoa(int pIdPessoa)
        {
            URL = txtURLAPI.Text;

            int IdPessoa = pIdPessoa;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                HttpResponseMessage responseMessage = await client.DeleteAsync(URL + IdPessoa);
                if (responseMessage.IsSuccessStatusCode)
                {
                    MessageBox.Show("Pessoa " + txtNome.Text + "de Id " + txtID.Text + " deletada com sucesso.");
                }
                else
                {
                    MessageBox.Show("Erro: " + responseMessage.StatusCode);
                }
            }
        }

        #region Eventos
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            ConsultarPessoaFisicaPorId(Convert.ToInt32(txtID.Text));
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            IncluirPessoaFisica();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            AtualizarPessoa();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            ExcluirPessoa(Convert.ToInt32(txtID.Text));
        }
        #endregion
    }
}

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Calendario : System.Web.UI.UserControl
{
  private int _anioDesde;
    private int _anioHasta;
    private DateTime _fechaSeleccionada;

    public int AnioDesde
    {
        get
        {
            return _anioDesde;
        }

        set
        {
            _anioDesde = value;

            cargarAnios();
        }
    }

    public int AnioHasta
    {
        get
        {
            return _anioHasta;
        }

        set
        {
            _anioHasta = value;

            cargarAnios();
        }
    }

    public DateTime FechaSeleccionada
    {
        get
        {
            return _fechaSeleccionada;
        }

        set
        {
            _fechaSeleccionada = value;

            int dia = _fechaSeleccionada.Day;
            int mes = _fechaSeleccionada.Month;
            int anio = _fechaSeleccionada.Year;

            cargarDias(DateTime.DaysInMonth(anio, mes));

            ddlDia.SelectedValue = dia.ToString();
            ddlMes.SelectedValue = mes.ToString();
            ddlAnio.SelectedValue = anio.ToString();
        }
    }

    public Calendario()
    {
        _anioDesde = DateTime.Today.Year - 10;
        _anioHasta = DateTime.Today.Year + 10;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarAnios();
            cargarMeses();

            FechaSeleccionada = DateTime.Today;
        }
        else
        {
            int diaSeleccionado = Convert.ToInt32(ddlDia.SelectedValue);
            int mesSeleccionado = Convert.ToInt32(ddlMes.SelectedValue);
            int anioSeleccionado = Convert.ToInt32(ddlAnio.SelectedValue);

            int ultimoDiaMes = DateTime.DaysInMonth(anioSeleccionado, mesSeleccionado);
            diaSeleccionado = diaSeleccionado <= ultimoDiaMes ? diaSeleccionado : ultimoDiaMes;

            FechaSeleccionada = new DateTime(anioSeleccionado, mesSeleccionado, diaSeleccionado);
        }
    }

    protected void cargarAnios()
    {
        ddlAnio.Items.Clear();

        for (int i = AnioDesde; i <= AnioHasta; i++)
        {
            ddlAnio.Items.Add(i.ToString());
        }
    }

    protected void cargarMeses()
    {
        ddlMes.Items.Clear();

        for (int i = 1; i <= 12; i++)
        {
            ddlMes.Items.Add(i.ToString());
        }
    }

    protected void cargarDias(int ultimoDiaMes)
    {
        ddlDia.Items.Clear();

        for (int i = 1; i <= ultimoDiaMes; i++)
        {
            ddlDia.Items.Add(i.ToString());
        }
    }
}
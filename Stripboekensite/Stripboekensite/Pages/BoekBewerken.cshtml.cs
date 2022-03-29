using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace Stripboekensite.Pages;

[Authorize(Roles = "Moderator")]
public class BoekBewerken : PageModel
{
    
#region variabels

    public BewerkItem bewerkitem;
    public List<Genre> Genres = new List<Genre>();
    public Stripboek stripboek = new Stripboek();
    public List<Uitgever> uitgevers = new List<Uitgever>();
    public List<Reeks> reeksen = new List<Reeks>();
    public List<Creator> creators = new List<Creator>();

    public List<CreatorStripboeken> CreatorStripboeken = new List<CreatorStripboeken>();

    public List<GenreStripboek> genrestripboeks = new List<GenreStripboek>();
    //public List<int> genreids { get; set; }

    public int stripid { get; set; }
    
#endregion

    //11 + 14 database calls

    public void OnGet(int stripboek_id)
    {

        stripid = stripboek_id;
        setlists(stripid);
        //gets id from cookie

    }

    public void OnPostUpdateStripboek(int stripboek_id,string titel, string isbn, int jaar1druk,string uitgever, int expleciet, int paginas, string reeksnaam, int reeksnum,string schrijvernaam, string tekenaarnaam, List<int> genreids)
    {
        setlists(stripboek_id); //cals upon set list to re get al data from database
        stripboek.Uitgever_id = stripboek.uitgever.Uitgever_id; //gets id from class object
        if (stripboek.reeks != null)  //gets id from class object of class object exist
        {
            stripboek.Reeks_id = stripboek.reeks.Reeks_id;
        }
        Stripboek addedstripboek = new Stripboek();
            
        //checks if anythings has been filled if nothing or 0 has been given as value, nothing will change
        if (titel != null)
        {
            stripboek.titel = titel;
        }
        if (isbn != null)
        {
                stripboek.isbn = isbn;
        }
        if (jaar1druk != 0)
        {
                stripboek.Uitgave1e_druk = jaar1druk;
        } 
        if (reeksnum != 0)
        {
                stripboek.Reeks_nr = reeksnum;
        }
        if (paginas != 0)
        {
                stripboek.Bladzijden = paginas;
        }
            
        if (uitgever != null)
        {
            stripboek.Uitgever_id = 0;
            foreach (var uitgevr in uitgevers)
            {
                if (uitgevr.Naam.ToLower() == uitgever.ToLower())
                {
                    stripboek.Uitgever_id = uitgevr.Uitgever_id;
                }
            }
            if (stripboek.Uitgever_id == 0)
            {
                //creates a uitgever connection to stripboek if it does not exist
                Uitgever newuitgever = new Uitgever();
                newuitgever.Naam = uitgever;
                Uitgever newruitgever = new UitgeverRepository().Add(newuitgever);
                stripboek.Uitgever_id = newruitgever.Uitgever_id;
            }
        }
            
        if (reeksnaam != null)
        {
            stripboek.Reeks_id = stripboek.reeks.Reeks_id;
            stripboek.Reeks_id = 0;
            foreach (var reeks in reeksen)
            {
                if (reeks.Reeks_titel.ToLower() == reeksnaam.ToLower()) 
                {
                    stripboek.Reeks_id = reeks.Reeks_id;
                    break;
                }
            }

            if (stripboek.Reeks_id == 0) 
            {
                //checks whether there is a reeks and will add with reeks 
                Reeks newrReeks = new Reeks();
                newrReeks.Reeks_titel = reeksnaam;
                stripboek.Reeks_id = new ReeksRepository().Add(newrReeks).Reeks_id;
            }

        }
            
        stripboek.expliciet = expleciet; //has always a value and input field cannot be empty

        addedstripboek = new StripboekRepository().Update(stripboek);

        //stripboiek creator connections
        if (schrijvernaam != null )
        {
            taakcreator(schrijvernaam, "schrijver", addedstripboek);
        }

        if (tekenaarnaam != null)
        {
            taakcreator(tekenaarnaam, "tekenaar", addedstripboek);

        }
            
        //genre stripboek connection
        List<GenreStripboek> genrelist = new List<GenreStripboek>();
        foreach (var genstrip in genrestripboeks)
        {
            genstrip.Stripboek_id = genstrip.Stripboek.Stripboek_id;
            genstrip.Genre_id = genstrip.genre.genre_id;
        }//sets id from class objects
        new GenreStripboekRepository().Delete(genrestripboeks); //deletes entire stripboek genre list so new one can be made 
            
        foreach (var genreid in genreids)
        {
            //adds a genre stripboek connection to database
            GenreStripboek stripboekgenre = new GenreStripboek();
            stripboekgenre.Stripboek_id = addedstripboek.Stripboek_id;
            stripboekgenre.Genre_id = genreid;
            genrelist.Add(stripboekgenre);

        } //adds each genre to genre list to be added to database
            
        new GenreStripboekRepository().Add(genrelist);
    }
        
    /// <summary>
    /// gets all the data from database. set to list and gets data from specific stripboek using stripboek_id. own method because multiple uses not only onget
    /// </summary>
    /// <param name="stripboek_id"></param>
    public void setlists(int stripboek_id)
    {
        bewerkitem=new BewerkItemRepository().Get(stripboek_id);
        
        Genres = bewerkitem.Genres;
        stripboek = bewerkitem.stripboek;
        creators = bewerkitem.creators;
        uitgevers = bewerkitem.uitgevers;
        reeksen = bewerkitem.reeksen;
        CreatorStripboeken = bewerkitem.CreatorStripboeken;
        genrestripboeks = bewerkitem.genrestripboeks;

    }

    /// <summary>
    /// adds creator stripboek connection. if creator does not exist, new creator will be made and addded to the database.
    /// removes creators with the same 'taak'
    /// </summary>
    /// <param name="naam"></param>
    /// <param name="taak"></param>
    /// <param name="stripboek"></param>
    public void taakcreator(string naam, string taak, Stripboek stripboek)
    {
        foreach (var creator in CreatorStripboeken)
        {
            //deletes the creator from the same taak
            creator.Creator_id = creator.Creator.Creator_id;
            creator.Stripboek_id = creator.Stripboek.Stripboek_id;
            if (creator.taak == taak)
            {
                new CreatorStripboekenRepository().Delete(creator.Creator.Creator_id, stripid);
            }
        }
        //creates new creator stripconnection 
        CreatorStripboeken creatorstrip = new CreatorStripboeken();
        foreach (var creator in creators)
        {
            if (creator.Creator_naam.ToLower() == naam.ToLower()) //if the creator already exist, get id of creator
            {
                creatorstrip.Stripboek_id = stripboek.Stripboek_id;
                creatorstrip.Creator_id = creator.Creator_id;
                creatorstrip.taak = taak; 
                break;
            }
        }
            
        if (creatorstrip.Creator_id == 0)//if creator does not exist and thus id=0 create new creator of name, add to database, and set values
        {
            Creator creator = new Creator();
            creator.Creator_naam = naam;
            Creator newcreator = new CreatorRepository().Add(creator);
            creators.Add(newcreator);
                
            creatorstrip.Stripboek_id = stripboek.Stripboek_id;
            creatorstrip.Creator_id = newcreator.Creator_id;
            creatorstrip.taak = taak;
        }
        //add stripboek creator connection to database
        new CreatorStripboekenRepository().Add(creatorstrip);
    }
}
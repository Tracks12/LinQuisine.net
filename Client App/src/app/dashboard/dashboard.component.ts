import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../api.service';
import { Recipe } from '../recipe';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  
})

export class DashboardComponent implements OnInit {
  
  foods:Recipe[] = [];
  constructor(private apiService:ApiService, private route:ActivatedRoute,public dialog: MatDialog) { }

  ngOnInit(): void {
    this.apiService.getAllRecipes().subscribe(r =>{
      //mettre r après le =
      this.foods = [
        {
            "id": 1,
            "userId": 1,
            "name": "Mousse express au Nutella",
            "nbParts": 6,
            "ingredients": [
                {
                    "name": "Mascarpone",
                    "quantity": 200,
                    "unit": "g"
                },
                {
                    "name": "Oeufs",
                    "quantity": 2
                },
                {
                    "name": "Nutella",
                    "quantity": 5,
                    "unit": "cuillères à soupe"
                },
                {
                    "name": "Sucre",
                    "quantity": 60,
                    "unit": "g"
                }
            ],
            "steps": [
                "Séparez les blancs et les jaunes d'oeufs. Battez les jaunes avec le sucre et ajoutez le mascarpone puis le Nutella.",
                "Montez les blancs en neige. Incorporez-les progressivement à la première préparation à l'aide d'une spatule.",
                "Versez la mousse dans des verrines et réservez au frais pendant au moins 3 heures. Servez bien froid."
            ]
        },
        {
            "id": 2,
            "userId": 1,
            "name": "Tarte au chèvre et saumon",
            "nbParts": 4,
            "ingredients": [
                {
                    "name": "Saumon frais",
                    "quantity": 250,
                    "unit": "g"
                },
                {
                    "name": "Oeufs",
                    "quantity": 5
                },
                {
                    "name": "Pâte feuilletée",
                    "quantity": 1,
                    "unit": "rouleau"
                },
                {
                    "name": "Chèvre",
                    "quantity": 1,
                    "unit": "bûche"
                },
                {
                    "name": "Crème fraîche épaisse",
                    "quantity": 20,
                    "unit": "cL"
                }
            ],
            "steps": [
                "Préchauffez le four à 180°. Etalez la pâte feuilletée dans un moule à tarte.",
                "Dans un grand récipient, écrasez le chèvre à l'aide d'une fourchette. Incorporez progressivement les oeufs.",
                "Découpez le saumon en dés. Ciselez l'aneth. Ajoutez à la préparation la crème, les dés de saumon, l'aneth, le sel et le poivre. Mélangez le tout.",
                "Versez la préparation sur la pâte feuilletée. Enfournez durant 40 minutes. Servez chaud ou tiède."
            ]
        }
    ];
    })
    this.route.params.subscribe(params=>{
      if(params['searchTerm']){
        this.foods = this.foods.filter((r =>
        r.name.toLowerCase().includes(params['searchTerm'].toLowerCase())));
      }
    })
  }




}

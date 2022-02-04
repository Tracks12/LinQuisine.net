export interface Recipe {
    id: number,
    userId:number,
    name: string,
    nbParts:number,
    ingredients: ({
        name: string;
        quantity: number;
        unit?: string;
        })[]
    steps:string[];
}

export interface Ingredients {
	name: string;
	quantity: number;
	unit?: string;
}

export interface Recipe {
	id: number;
	userId: number;
	name: string;
	nbParts: number;
	ingredients: Ingredients[];
	steps: string[];
}

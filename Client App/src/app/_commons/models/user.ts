export interface Profile {
	id: number;
	username: string;
	mail: string;
}

export interface Login {
	profile: Profile;
	token: string;
}

export interface LoginBody {
	username: string;
	password: string;
};

export interface RegisterBody {
	username: string;
	mail: string;
	password: string;
};

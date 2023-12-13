export interface LoginModel {
    email:string;
    senha:string;
};

export interface AuthResponse{
    token: string,
    autenticado: boolean,
    expiration: string,
    message: string,
    aspNetUserID: string
}

export interface RegistroModel{
    nome: string;
    sobreNome:string;
    email:string;
    senha:string;
    confirmarSenha:string;
    peso: number | null;
    idade:number | null;
    altura:number | null;
    dataCriacao: Date;
    roleNome: string | null; 
}

export interface PerfilModel{
    nome: string;
    sobreNome:string;
    email:string;
    peso: number | null;
    idade:number | null;
    altura:number | null;
    dataCriacao: Date;
    roleNome: string | null | undefined; 
}

export interface TreinoModel{
 
    treinoId: number;
    nomeTreino: string;
    descricaoTreino: string;
    dataCriacao: Date;
    exercicios: any;
    nomeExercicio: string;
}

export interface ExercicioModel{
    exercicioID: number;
    nome:string;
    peso: number;
    repeticoes:number;
    series:number;
    treinoId: number;
    descricao:string;
    dataCriacao:Date;
    drop: boolean;
    biset: boolean;
}
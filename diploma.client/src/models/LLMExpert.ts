import ExpertEvaluation from "./ExpertEvaluation";

export default class LLMExpert {
    expertId: number;
    name: string;
    url: string;
    expertEvaluation: ExpertEvaluation[];

    constructor() {
        this.expertId = 0;
        this.name = "";
        this.url = "";
        this.expertEvaluation = [];
    }
}
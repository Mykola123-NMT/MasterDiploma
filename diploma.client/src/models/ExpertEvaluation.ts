import LLMExpert from "./LLMExpert";
import Product from "./Product";

export default class ExpertEvaluation {
    evaluationId: number;
    productId: string;
    product: Product;
    expertId: number;
    expert: LLMExpert;
    priceStrategy: number;
    demand: number;
    quality: number;
    priceQuality: number;
    timestamp: Date;
    advantages: string[];
    disadvantages: string[];

    constructor() {
        this.evaluationId = 0;
        this.productId = "";
        this.product = new Product();
        this.expertId = 0;
        this.expert = new LLMExpert();
        this.priceStrategy = 0;
        this.demand = 0;
        this.quality = 0;
        this.priceQuality = 0;
        this.timestamp = new Date();
        this.advantages = [];
        this.disadvantages = [];
    }
}
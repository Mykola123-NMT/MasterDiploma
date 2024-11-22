import Product from "./Product";

export default class ConsensusEvaluation {
    consensusEvaluationId: number;
    priceStrategy: number;
    demand: number;
    quality: number;
    priceQuality: number;
    timestamp: Date;
    productId: string;
    product?: Product | null;

    constructor() {
        this.consensusEvaluationId = 0;
        this.productId = "";
        this.product = null;
        this.priceStrategy = 0;
        this.demand = 0;
        this.quality = 0;
        this.priceQuality = 0;
        this.timestamp = new Date();
    }
}
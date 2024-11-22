import { Button, Flex, Modal } from "antd"
import { ExpertOpinionListComponent } from "./ExpertOpinionList"
import Product from "../models/Product";
import { useState } from 'react';
import { ProductFormComponent } from "./ProductForm";
import ExpertEvaluation from "../models/ExpertEvaluation";
import evaluationApi from "../api/evaluationApi";
import { OpinionComponent } from "./Opinion";
import { EvaluationResultsFormComponent } from "./EvaluationResultsForm";

export const EvaluationComponent = () => {
    const [product, setProduct] = useState<Product>(new Product());
    const [, setExpertEvaluations] = useState<ExpertEvaluation[]>([]);
    const [consensusOpinion, setConsensusOpinion] = useState<ExpertEvaluation>(new ExpertEvaluation());
    const [expectedSales, setExpectedSales] = useState(0);
    const [isProductModalOpen, setIsProductModalOpen] = useState(false);
    const [isExpertOpinionListVisible, setIsExpertOpinionListVisible] = useState(false);
    const [isConsensusOpinionVisible, setIsConsensusOpinionVisible] = useState(false);
    const [isRegressionModalOpen, setIsRegressionModalOpen] = useState(false);

    const handleProductSubmit = (submittedProduct: Product) => {
        setProduct(submittedProduct);
        setIsProductModalOpen(false);
        setIsExpertOpinionListVisible(true);
    };

    const handleExpertEvaluations = async (submittedEvaluations: ExpertEvaluation[]) => {
        setExpertEvaluations(submittedEvaluations);

        const responseConsensusOpinion = await evaluationApi.getConsensusOpinion(submittedEvaluations);

        const expertConsensusOpinion = new ExpertEvaluation();
        expertConsensusOpinion.priceStrategy = responseConsensusOpinion.result.priceStrategy;
        expertConsensusOpinion.demand = responseConsensusOpinion.result.demand;
        expertConsensusOpinion.quality = responseConsensusOpinion.result.quality;
        expertConsensusOpinion.priceQuality = responseConsensusOpinion.result.priceQuality;

        setConsensusOpinion(expertConsensusOpinion);
        setIsConsensusOpinionVisible(true);

        const responseModel = await evaluationApi.getRegressionResult(responseConsensusOpinion.result);
        setExpectedSales(Math.ceil(responseModel.score));
        setIsRegressionModalOpen(true);        
    }

    const toggleModalVisibility = () => {
        setIsProductModalOpen((prev) => !prev); 
    };

    const handleEvaluateFormCancel = () => {
        setIsProductModalOpen(false); 
    };

    const handleResultFormCancel = () => {
        setIsRegressionModalOpen(false);
    };

    return (
        <Flex vertical gap="middle">
            <Button type="primary" onClick={toggleModalVisibility}>
                Evaluate Product
            </Button>
            <Modal
                title="Evaluate Product"
                open={isProductModalOpen}
                onCancel={handleEvaluateFormCancel}
                footer={null} 
            >
                <ProductFormComponent onSubmit={handleProductSubmit} />
            </Modal>
            {isExpertOpinionListVisible && <ExpertOpinionListComponent
                product={product}
                performEvaluation={isExpertOpinionListVisible}
                handleExpertEvaluations={handleExpertEvaluations}
            />}
            {isConsensusOpinionVisible && <OpinionComponent expertOpinion={consensusOpinion} isExpert={false} />}
            <Modal
                title="Evaluation results"
                open={isRegressionModalOpen}
                onCancel={handleResultFormCancel}
                footer={null}
            >
                <EvaluationResultsFormComponent product={product}
                    consensusOpinion={consensusOpinion} expectedSales={expectedSales} />
            </Modal>
        </Flex>
    )
}

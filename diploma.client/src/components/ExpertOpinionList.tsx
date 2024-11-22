import { Flex, Spin } from "antd";
import ExpertEvaluation from "../models/ExpertEvaluation";
import { useState, useEffect } from 'react';
import evaluationApi from "../api/evaluationApi";
import Product from "../models/Product";
import { OpinionComponent } from "./Opinion";

const contentStyle: React.CSSProperties = {
    padding: 100,
    background: 'rgba(0, 0, 0, 0.05)',
    borderRadius: 4,
};

const content = <div style={contentStyle} />;

interface ExpertOpinionListComponentProps {
    product: Product;
    performEvaluation: boolean;
    handleExpertEvaluations: (expertEvaluations: ExpertEvaluation[]) => void;
}

export const ExpertOpinionListComponent = ({ product, performEvaluation, handleExpertEvaluations }: ExpertOpinionListComponentProps) => {
    const [expertOpinions, setExpertOpinions] = useState<ExpertEvaluation[]>([]);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        if (performEvaluation) {
            loadOpinions();
        }
    }, [performEvaluation]);

    const loadOpinions = async () => {
        setIsLoading(true);
        const response = await evaluationApi.getExpertOpinions(product);
        setExpertOpinions(response.$values);
        handleExpertEvaluations(response.$values);
        setIsLoading(false);
    }

    return (
        <Flex gap="35px" wrap justify="space-around">
            {isLoading ? (
                <>
                    <Spin tip="Loading expert opinion" size="large">
                        {content}
                    </Spin>
                    <Spin tip="Loading expert opinion" size="large">
                        {content}
                    </Spin>
                    <Spin tip="Loading expert opinion" size="large">
                        {content}
                    </Spin>
                </>                
            ) : (
                expertOpinions.map((expertOpinion, index) => (
                    <OpinionComponent key={index} expertOpinion={expertOpinion} isExpert={true} />
                ))
            )}
        </Flex>
    );
}

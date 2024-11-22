import ExpertEvaluation from "../models/ExpertEvaluation";
import { Card, Tag, Typography } from "antd";

const { Title, Text } = Typography;

interface ExpertOpinionComponentProps {
    expertOpinion: ExpertEvaluation;
    isExpert: boolean;
}

export const OpinionComponent = ({ expertOpinion, isExpert }: ExpertOpinionComponentProps) => {
    return (
        <Card
            title={<Title level={4}>{isExpert ? "Expert Opinion" : "Consensus Opinion"}</Title>}
            bordered={true}
            style={{ width: 400, margin: "16px auto" }}
        >
            {isExpert ? (
                <div>
                    <Text strong>Expert:</Text> <Tag color="blue">{expertOpinion.expert.name}</Tag>
                </div>
            ) :
                <></>
            }            
            <div>
                <Text strong>Price Strategy:</Text> <Text>{expertOpinion.priceStrategy}</Text>
            </div>
            <div>
                <Text strong>Demand:</Text> <Text>{expertOpinion.demand}</Text>
            </div>
            <div>
                <Text strong>Quality:</Text> <Text>{expertOpinion.quality}</Text>
            </div>
            <div>
                <Text strong>Price-Quality Ratio:</Text> <Text>{expertOpinion.priceQuality}</Text>
            </div>
            <div>
                <Text strong>Timestamp:</Text> <Text>{new Date(expertOpinion.timestamp).toLocaleString()}</Text>
            </div>
        </Card>
    );
};

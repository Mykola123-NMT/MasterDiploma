import { CancelToken } from 'axios';
import ExpertEvaluation from '../models/ExpertEvaluation';
import ConsensusEvaluation from '../models/ConsensusEvaluation';
import api from './api';
import handleError from './errorHandler';
import Product from '../models/Product';


const getExpertOpinions = async (product: Product, cancelToken?: CancelToken) => {
    const response = await api.post(`Evaluation/ExpertOpinions`, product, cancelToken)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
            return null;
        });
    return response;
}

const getConsensusOpinion = async (opinions: ExpertEvaluation[], cancelToken?: CancelToken) => {
    const response = await api.post(`Evaluation/ConsensusOpinion`, opinions, cancelToken)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
            return null;
        });
    return response;
}

const getRegressionResult = async (consensusOpinion: ConsensusEvaluation, cancelToken?: CancelToken) => {
    const response = await api.post(`Evaluation/RegressionResult`, consensusOpinion, cancelToken)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
            return null;
        });
    return response;
}

export default { getExpertOpinions, getConsensusOpinion, getRegressionResult }
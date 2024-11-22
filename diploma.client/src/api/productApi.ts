import { CancelToken } from 'axios';
import api from './api';
import handleError from './errorHandler';


const getProductsPerPage = async (pageNumber: number, pageSize: number, cancelToken?: CancelToken) => {
    const response = await api.get(`Products/GetProductsPerPage/${pageNumber}/${pageSize}`, cancelToken)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
            return null;
        });
    return response;
}

const getProduct = async (id: number, cancelToken: CancelToken) => {
    const response = await api.get(`Products/GetProductsPerPage/${id}`, cancelToken)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
            return null;
        });
    return response;
}

export default { getProductsPerPage, getProduct }
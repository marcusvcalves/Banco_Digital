import React from 'react';

interface PaginationProps {
    currentPage: number;
    totalPages: number;
    onPageChange: (pageNumber: number) => void;
}

export const Pagination: React.FC<PaginationProps> = ({ currentPage, totalPages, onPageChange }) => {
    const renderPageNumbers = () => {
        const pageNumbers = [];
        const maxVisiblePages = 5;

        let startPage = Math.max(1, Math.min(currentPage - Math.floor(maxVisiblePages / 2), totalPages - maxVisiblePages + 1));
        let endPage = Math.min(totalPages, startPage + maxVisiblePages - 1);

        if (startPage > 1) {
            pageNumbers.push(
                <li key={1} className={`bg-gray-200 text-gray-600 hover:bg-gray-300 px-4 py-2 rounded-full cursor-pointer`} onClick={() => onPageChange(1)}>
                    1
                </li>
            );
        }

        if (startPage > 2) {
            pageNumbers.push(
                <li key="first-ellipsis" className={`bg-gray-200 text-gray-600 hover:bg-gray-300 px-4 py-2 rounded-full cursor-pointer`}>
                    ...
                </li>
            );
        }

        for (let i = startPage; i <= endPage; i++) {
            pageNumbers.push(
                <li key={i} className={`${currentPage === i ? 'bg-blue-500 text-white' : 'bg-gray-200 text-gray-600 hover:bg-gray-300'} px-4 py-2 rounded-full cursor-pointer`} onClick={() => onPageChange(i)}>
                    {i}
                </li>
            );
        }

        if (endPage < totalPages - 1) {
            pageNumbers.push(
                <li key="last-ellipsis" className={`bg-gray-200 text-gray-600 hover:bg-gray-300 px-4 py-2 rounded-full cursor-pointer`}>
                    ...
                </li>
            );
        }

        if (endPage < totalPages) {
            pageNumbers.push(
                <li key={totalPages} className={`bg-gray-200 text-gray-600 hover:bg-gray-300 px-4 py-2 rounded-full cursor-pointer`} onClick={() => onPageChange(totalPages)}>
                    {totalPages}
                </li>
            );
        }

        return pageNumbers;
    };

    return (
        <div className="pagination mt-4 flex justify-center items-center">
            <ul className="flex space-x-2">
                {renderPageNumbers()}
            </ul>
        </div>
    );
};

﻿using MediatR;
using Catalog.Application.Responses;


namespace Catalog.Application.Queries;

public class GetAllProductsQuery: IRequest<IList<ProductResponse>>
{
    
}
import { SearchRequestModel, SearchResponseModel } from '../models';

export async function search(query: string): Promise<string | undefined> {
  const requestModel: SearchRequestModel = { prompt: query };
  const endpoint = `/hybridrag`;
  const response = await fetch(endpoint, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(requestModel),
  });
  if (!response.ok) {
    console.error(`Failed to get search response: ${response.statusText}`);
    return undefined;
  }
  const responseModel = (await response.json()) as SearchResponseModel;
  return responseModel.response;
}

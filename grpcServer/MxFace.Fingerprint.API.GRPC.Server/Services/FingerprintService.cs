using Grpc.Core;
using MxFace.Fingerprint.gRPC;
using MxFace.SDK.Fingerprint.Interfaces;
using static MxFace.Fingerprint.gRPC.FingerprintService;

namespace MxFace.Fingerprint.API.GRPC.Server.Services
{
    public class FingerprintService : FingerprintServiceBase
    {
        private readonly ISearch _searchService;

        public FingerprintService(ISearch searchService)
        {
            _searchService = searchService;
        }

        public override async Task<EnrollResponse> Enroll(EnrollRequest request, ServerCallContext context)
        {
            try
            {
                var enroll = await _searchService.Enroll(
                    request.TemplateData.ToByteArray(),
                    request.PersonId,
                    request.Group);

                if (enroll != null)
                {
                    return new EnrollResponse
                    {
                        Code = (int)enroll.Code,
                        Message = enroll.Message
                    };
                }
                return new EnrollResponse();
            }
            catch (Exception ex)
            {
                return new EnrollResponse
                {
                    Message = ex.Message
                };
            }
        }

        public override async Task<SearchResponse> Search(SearchRequest request, ServerCallContext context)
        {
            try
            {
                var search = await _searchService.Search(
                    request.TemplateData.ToByteArray(),
                    request.Group);

                if (search != null)
                {
                    return new SearchResponse
                    {
                        MatchingScore = search.FirstOrDefault().MatchingScore
                    };
                }

                return new SearchResponse();
            }
            catch (Exception ex)
            {
                return new SearchResponse
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        public override async Task<VerifyResponse> Verify(VerifyRequest request, ServerCallContext context)
        {
            try
            {
                var verify = await _searchService.Verify(
                    request.SourceTemplate.ToByteArray(),
                    request.TargetTemplate.ToByteArray());

                if (verify != null)
                {
                    return new VerifyResponse
                    {
                        Score = verify.Score
                    };
                }
                return new VerifyResponse();
            }
            catch(Exception ex)
            {
                return new VerifyResponse
                {
                    ErrorMessage= ex.Message
                };
            }
        }
    }
}

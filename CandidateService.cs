using System;
using System.Collections.Generic;
using System.Text;

namespace JRojas
{
    public class CandidateService
    {
        private static List<Candidate> _candidates = new List<Candidate>();

        public List<Candidate> GetCandidates()
        {
            return _candidates;
        }

        public void AddCandidate(Candidate candidate)
        {
            _candidates.Add(candidate);
        }
    }

}
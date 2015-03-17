using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteServer.ZQRL
{
    public class NationalWelfareInfo
    {
        private long id;
        private long personid;
        private long agreementid;
        private DateTime feeDate;
        private float selfEndowmentInsBase;
        private float selfEndowmentInsRate;
        private float selfEndowmentIns;
        private float cmpEndowmentInsBase;
        private float cmpEndowmentInsRate;
        private float cmpEndowmentIns;
        private float selfMedicalInsBase;
        private float selfMedicalInsrRate;
        private float selfMedicalIns;
        private float cmpMedicalInsBase;
        private float cmpMedicalInsRate;
        private float cmpMedicalIns;
        private float selfJoblessInsBase;
        private float selfJoblessInsRate;
        private float selfJoblessIns;
        private float cmpJoblessInsBase;
        private float cmpfJoblessInsRate;
        private float cmpJoblessIns;
        private float cmpworkInjuredBase;
        private float cmpworkInjuredRate;
        private float cmpworkInjured;
        private float cmpBirthBase;
        private float cmpBirthRate;
        private float cmpBirth;
        private float selfIllnessBase;
        private float selfIllnessRate;
        private float selfIllness;
        private float cmpIllnessBase;
        private float cmpIllnessRate;
        private float cmpIllness;
        private float cmpDisableBase;
        private float cmpDisableRate;
        private float cmpDisable;
        private float selfFundBase;
        private float selfFundRate;
        private float selfFund;
        private float cmpFundBase;
        private float cmpFundRate;
        private float cmpFund;
        private float selfAddFundBase;
        private float selfAddFundRate;
        private float selfAddFund;
        private float cmpAddFundBase;
        private float cmpAddFundRate;
        private float cmpAddFund;
        private DateTime effectDate;
        private string SettlementCode;

        public long ID
        {
            set { id = value; }
            get { return id; }
        }
        public long PersonId
        {
            set { personid = value; }
            get { return personid; }
        }
        public long AgreementId
        {
            set { agreementid = value; }
            get { return agreementid; }
        }
        public DateTime FeeDate
        {
            set { feeDate = value; }
            get { return feeDate; }
        }
        public float SelfEndowmentInsBase
        {
            set { selfEndowmentInsBase = value; }
            get { return selfEndowmentInsBase; }
        }
        public float SelfEndowmentInsRate
        {
            set { selfEndowmentInsRate = value; }
            get { return selfEndowmentInsRate; }
        }
        public float SelfEndowmentIns
        {
            set { selfEndowmentIns = value; }
            get { return selfEndowmentIns; }
        }
        public float CmpEndowmentInsBase
        {
            set { cmpEndowmentInsBase = value; }
            get { return cmpEndowmentInsBase; }
        }
        public float CmpEndowmentInsRate
        {
            set { cmpEndowmentInsRate = value; }
            get { return cmpEndowmentInsRate; }
        }
        public float CmpEndowmentIns
        {
            set { cmpEndowmentIns = value; }
            get { return cmpEndowmentIns; }
        }
        public float SelfMedicalInsBase
        {
            set { selfMedicalInsBase = value; }
            get { return selfMedicalInsBase; }
        }
        public float SelfMedicalInsrRate
        {
            set { selfMedicalInsrRate = value; }
            get { return selfMedicalInsrRate; }
        }
        public float SelfMedicalIns
        {
            set { selfMedicalIns = value; }
            get { return selfMedicalIns; }
        }
        public float CmpMedicalInsBase
        {
            set { cmpMedicalInsBase = value; }
            get { return cmpMedicalInsBase; }
        }
        public float CmpMedicalInsRate
        {
            set { cmpMedicalInsRate = value; }
            get { return cmpMedicalInsRate; }
        }
        public float CmpMedicalIns
        {
            set { cmpMedicalIns = value; }
            get { return cmpMedicalIns; }
        }
        public float SelfJoblessInsBase
        {
            set { selfJoblessInsBase = value; }
            get { return selfJoblessInsBase; }
        }
        public float SelfJoblessInsRate
        {
            set { selfJoblessInsRate = value; }
            get { return selfJoblessInsRate; }
        }
        public float SelfJoblessIns
        {
            set { selfJoblessIns = value; }
            get { return selfJoblessIns; }
        }
        public float CmpJoblessInsBase
        {
            set { cmpJoblessInsBase = value; }
            get { return cmpJoblessInsBase; }
        }
        public float CmpfJoblessInsRate
        {
            set { cmpfJoblessInsRate = value; }
            get { return cmpfJoblessInsRate; }
        }
        public float CmpJoblessIns
        {
            set { cmpJoblessIns = value; }
            get { return cmpJoblessIns; }
        }
        public float CmpworkInjuredBase
        {
            set { cmpworkInjuredBase = value; }
            get { return cmpworkInjuredBase; }
        }
        public float CmpworkInjuredRate
        {
            set { cmpworkInjuredRate = value; }
            get { return cmpworkInjuredRate; }
        }
        public float CmpworkInjured
        {
            set { cmpworkInjured = value; }
            get { return cmpworkInjured; }
        }
        public float CmpBirthBase
        {
            set { cmpBirthBase = value; }
            get { return cmpBirthBase; }
        }
        public float CmpBirthRate
        {
            set { cmpBirthRate = value; }
            get { return cmpBirthRate; }
        }
        public float CmpBirth
        {
            set { cmpBirth = value; }
            get { return cmpBirth; }
        }
        public float SelfIllnessBase
        {
            set { selfIllnessBase = value; }
            get { return selfIllnessBase; }
        }
        public float SelfIllnessRate
        {
            set { selfIllnessRate = value; }
            get { return selfIllnessRate; }
        }
        public float SelfIllness
        {
            set { selfIllness = value; }
            get { return selfIllness; }
        }
        public float CmpIllnessBase
        {
            set { cmpIllnessBase = value; }
            get { return cmpIllnessBase; }
        }
        public float CmpIllnessRate
        {
            set { cmpIllnessRate = value; }
            get { return cmpIllnessRate; }
        }
        public float CmpIllness
        {
            set { cmpIllness = value; }
            get { return cmpIllness; }
        }
        public float CmpDisableBase
        {
            set { cmpDisableBase = value; }
            get { return cmpDisableBase; }
        }
        public float CmpDisableRate
        {
            set { cmpDisableRate = value; }
            get { return cmpDisableRate; }
        }
        public float CmpDisable
        {
            set { cmpDisable = value; }
            get { return cmpDisable; }
        }
        public float SelfFundBase
        {
            set { selfFundBase = value; }
            get { return selfFundBase; }
        }
        public float SelfFundRate
        {
            set { selfFundRate = value; }
            get { return selfFundRate; }
        }
        public float SelfFund
        {
            set { selfFund = value; }
            get { return selfFund; }
        }
        public float CmpFundBase
        {
            set { cmpFundBase = value; }
            get { return cmpFundBase; }
        }
        public float CmpFundRate
        {
            set { cmpFundRate = value; }
            get { return cmpFundRate; }
        }
        public float CmpFund
        {
            set { cmpFund = value; }
            get { return cmpFund; }
        }
        public float SelfAddFundBase
        {
            set { selfAddFundBase = value; }
            get { return selfAddFundBase; }
        }
        public float SelfAddFundRate
        {
            set { selfAddFundRate = value; }
            get { return selfAddFundRate; }
        }
        public float SelfAddFund
        {
            set { selfAddFund = value; }
            get { return selfAddFund; }
        }
        public float CmpAddFundBase
        {
            set { cmpAddFundBase = value; }
            get { return cmpAddFundBase; }
        }
        public float CmpAddFundRate
        {
            set { cmpAddFundRate = value; }
            get { return cmpAddFundRate; }
        }
        public float CmpAddFund
        {
            set { cmpAddFund = value; }
            get { return cmpAddFund; }
        }
        public DateTime EffectDate
        {
            set { effectDate = value; }
            get { return effectDate; }
        }
        public string SettleMentCode
        {
            set { SettlementCode = value; }
            get { return SettlementCode; }
        }
    }
}

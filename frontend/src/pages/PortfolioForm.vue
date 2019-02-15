<template>
  <layout-baseline>
    <v-container>
      <v-layout row wrap>
        <v-flex xs12 md4 pa-1>
          <profile 
            :account="account"
          />
        </v-flex>
        <v-flex xs12 md7 offset-md1 pa-1>
          <v-container fluid grid-list-sm>
            <v-layout row wrap>
              <timeline-certificate
                v-for="(certificate, index) in certificates"
                :key="index"
                :certificate="certificate"
              />
              <v-flex xs12 md6>
                <div class="certificate-add">
                  <issuer-selecter />
                </div>
              </v-flex>
            </v-layout>
          </v-container>
        </v-flex>
      </v-layout>
    </v-container>
  </layout-baseline>
</template>

<script>
import { mapActions, mapState } from 'vuex'
import TimelineCertificate from '@/components/portfolioForm/TimelineCertificate'
import Profile from '@/components/portfolioForm/Profile'
import IssuerSelecter from '@/components/portfolioForm/IssuerSelecter'

export default {
  components: {
    TimelineCertificate,
    Profile,
    IssuerSelecter
  },
  computed: {
    ...mapState('auth', ['account']),
    ...mapState('certificate', ['certificates'])
  },
  created: function () {
    this.initialize()
  },
  methods: {
    ...mapActions({
      fetchCertificates: 'certificate/fetchCertificates'
    }),
    initialize: function () {
      this.fetchCertificates()
    }
  }
}
</script>

<style scoped>
.certificate-add{
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 12vh;
  border: dashed 1px #489DAB;
}
</style>


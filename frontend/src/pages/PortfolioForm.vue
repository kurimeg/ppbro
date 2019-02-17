<template>
  <layout-baseline
    :headerName="this.headerName"
    :isUser="this.isUser"
  >
    <v-container>
      <v-layout row wrap>
        <v-flex xs12 md4 pa-1>
          <profile
            :account="account"
          />
        </v-flex>
        <v-flex xs12 md7 offset-md1 pa-1>
          <h3 class="certificate-text"></h3>
          <v-container fluid grid-list-sm>
            <v-layout row wrap>
                <timeline-certificate
                  v-for="certificate in certificates"
                  :key="certificate.issuerName"
                  :certificate="certificate"
                  @selected="onSelected"
                  class="certificate"
                />
              <v-flex xs12 md6 pa-2>
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
import TimelineCertificate from '@/components/TimelineCertificateEditor'
import Profile from '@/components/ProfileEditor'
import IssuerSelecter from '@/components/IssuerSelecter'

export default {
  components: {
    TimelineCertificate,
    Profile,
    IssuerSelecter
  },
  data: function () {
    return {
      headerName: 'プロフィール編集',
      isUser: true,
      selectedCertificates: []
    }
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
      publishCertificates: 'certificate/publishCertificates',
      fetchProofs: 'certificate/fetchProofs'
    }),
    initialize: function () {
      this.fetchProofs()
    },
    onPublish: function () {
      console.log(this.selectedCertificates)
    },
    onSelected: function (certificate) {
      this.selectedCertificates.push(certificate)
    }
  }
}
</script>

<style scoped>
.certificate{
  pointer-events: none;
}
.certificate-add{
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 20vh;
  border: dashed 1px #489DAB;
}
.certificate-text{
  color: #707070;
}
</style>

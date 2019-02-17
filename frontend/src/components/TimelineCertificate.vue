<template>
  <v-timeline-item
    color="accent"
  >
    <v-layout row wrap pt-3>
      <v-flex xs12 md3>
        <strong>{{ certificate.issueDate }}</strong>
      </v-flex>
      <v-flex xs12 md9>
        <v-flex xs12 class="certificate-container" pa-2>
        <v-card height="20vh">
          <v-layout row wrap>
            <v-flex xs3 md3 pa-2>
              <v-img
                :src="!this.certificate.src ? require('@/assets/university.svg') : this.certificate.src"
                position="left"
                contain
              ></v-img>
            </v-flex>
            <v-flex xs7 md8 pa-2>
                {{ certificate.issuerName }}<br/>
                {{ certificate.detail + ' ' + certificate.type }}
            </v-flex>
          </v-layout>
        </v-card>

        <v-flex v-if="certificate.issuerSign" offset-xs10 xs2 class="certificate-badge">
          <v-icon color="#FF8160" size="50">fas fa-award</v-icon>
        </v-flex>
        </v-flex>
      </v-flex>
    </v-layout>
  </v-timeline-item>
</template>

<script>
export default {
  props: {
    certificate: Object
  }
}
</script>

<style scoped>
.certificate-container{
  position: relative;
}
.certificate-badge{
  position: absolute;
  top: 0;
  right: 0;
}
</style>
